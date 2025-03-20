using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private NavMeshAgent agent;
    private Chair chairToSit;

    [SerializeField]
    private GameObject dialoguePanelPrefab;
    private GameObject dialoguePanel;

    public List<CoffeeItem> customerPreferences = new List<CoffeeItem>();

    public int maxPreferences = 1;

    private bool playerActive = false;
    private bool isSitting = false;

    private float orderTimer = 0f;
    private bool isTimerRunning = false;

    private bool wasServed = false;
    private bool wasAccepted = false;
    private bool hasSpoken = false;

    private bool konec = false;


    [SerializeField] private float timeLimit = 300f;

    private ItemContainer playerInv;

    private SpriteRenderer customerSprite;

    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GameManager.Instance.cashRegister.GetComponent<BoxCollider2D>();
        customerSprite = GetComponent<SpriteRenderer>();
        playerInv = GameManager.Instance.invContainer;
        GenerateCustomPreferences();
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false; //NEMAZAT jinak zmizí sprite
        GoToCashRegister();

        if (slider != null)
        {
            slider.maxValue = timeLimit;
            slider.value = timeLimit;
            slider.gameObject.SetActive(false);
        }
    }

    private void GoToCashRegister()
    {
        Vector2 colliderCenter = boxCollider.bounds.center;
        Vector2 colliderSize = boxCollider.bounds.size;

        float randomX = UnityEngine.Random.Range(colliderCenter.x - colliderSize.x / 2, colliderCenter.x + colliderSize.x / 2);
        float randomY = UnityEngine.Random.Range(colliderCenter.y - colliderSize.y / 2, colliderCenter.y + colliderSize.y / 2);

        Vector3 targetPosition = new Vector3(randomX, randomY, agent.transform.position.z);

        agent.SetDestination(targetPosition);
    }

    public void GenerateCustomPreferences()
    {
        customerPreferences.Clear();
        GeneratePreferences(GameManager.Instance.menuItems, maxPreferences);
    }

    private void GeneratePreferences(List<CoffeeItem> items, int preferenceCount)
    {
        for (int i = 0; i < preferenceCount && items.Count > 0; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, items.Count);
            customerPreferences.Add(items[randomIndex]);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (playerActive && Input.GetKeyDown(KeyCode.F) && !hasSpoken)
        {
            hasSpoken = true;
            if (!wasAccepted)
            { 
                wasAccepted = true;
                //GameManager.Instance.dialogue.customer = this;
                //GameManager.Instance.dialogue.TurnOnDialogue(true);

                dialoguePanel = Instantiate(dialoguePanelPrefab, GameManager.Instance.ui.transform);
                dialoguePanel.GetComponent<DialogueController>().customer = this;
                dialoguePanel.GetComponent<DialogueController>().TurnOnDialogue(true);
            }
        }

        if (playerActive && isSitting && Input.GetKeyDown(KeyCode.F))
        {
            isTimerRunning = false;
            chairToSit.ocupied = false;
            FulfillCustomerOrder();
            StartCoroutine(ExitCafe());
            wasAccepted = false;
        }

        if (playerActive && chairToSit == null && dialoguePanel.GetComponent<DialogueController>().wasAccepted)
        {
            
            GoToTable();
            isSitting = true;
            isTimerRunning = true;

            if (slider != null)
            {
                slider.gameObject.SetActive(true); // Show the slider when the timer starts
            }

            //GameManager.Instance.dialogue.customer = null;
            dialoguePanel.GetComponent<DialogueController>().customer = null;
        }
        else if (hasSpoken && !dialoguePanel.GetComponent<DialogueController>().wasAccepted && dialoguePanel.GetComponent<DialogueController>().wasDeclined) 
        {
            StartCoroutine(ExitCafe());
        }
        

        if (isTimerRunning)
        {
            orderTimer += Time.deltaTime;
        }

        if (slider != null)
        {
            slider.value = timeLimit - orderTimer;
        }

        if (orderTimer >= timeLimit) 
        {
            isTimerRunning = false;
            StartCoroutine(ExitCafe());
        }

        if(GameManager.Instance.gameObject.GetComponent<DayNight>().time >= 79200)
        {
            StartCoroutine(ExitCafe());
        }
    }

    private IEnumerator ExitCafe()
    {
        if (!konec)
        {
            GameManager.Instance.gameObject.GetComponent<CustomerSpawning>().RemoveCustomer();
            konec = true;
        }
        
        GetPayed();
        agent.SetDestination(GameManager.Instance.SpawnPoint.transform.position);
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
        //GameManager.Instance.dialogue.ResetOrder(false);
        dialoguePanel.GetComponent<DialogueController>().ResetOrder(false);
        Debug.Log(orderTimer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerActive = false;
        }
    }

    public void GoToTable()
    {
        chairToSit = GetAvailableChair();
        if (chairToSit != null) 
        {
            chairToSit.ocupied = true;
            agent.SetDestination(chairToSit.transform.position);
        }
        else
        {
            Debug.Log("NoChairAvailable");
        }
    }

    Chair GetAvailableChair()
    {
        /*foreach (Table table in GameManager.Instance.tables) 
        {
            Chair availableChair = table.GetAvailableChair();
            if(availableChair != null)
            {
                return availableChair;
            }
        }
        return null;*/

        int randomTable = UnityEngine.Random.Range(0,7);
        Chair availableChair = GameManager.Instance.tables[randomTable].GetAvailableChair();
        if (availableChair != null)
        {
            return availableChair;
        }

        return null;
    }

    private void GetPayed()
    {
        if (wasServed)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if (orderTimer < timeLimit / 4)
            {
                GameManager.Instance.money += 100;
            }
            else if (orderTimer < timeLimit / 3)
            {
                GameManager.Instance.money += 75;
            }
            else if (orderTimer < timeLimit / 2)
            {
                GameManager.Instance.money += 50;
            }
        }
    }

    public bool FulfillCustomerOrder()
    {
        foreach (var preferredItem in customerPreferences)
        {
            if (!HasRequiredItems(preferredItem))
            {
                StartCoroutine(ChangeColor(Color.red));
                Debug.Log("Order cannot be finished");
                return false;
            }
        }
        foreach (var preferredItem in customerPreferences)
        {
            
            wasServed = true;
            RemoveItemFromInventory(preferredItem);
            StartCoroutine(ChangeColor(Color.green));
        }

        

        Debug.Log("Order finished successfully!");
        return true;
    }

    private IEnumerator ChangeColor(Color colorToChangeTo)
    {
        customerSprite.color = colorToChangeTo;
        yield return new WaitForSeconds(0.5f);
        customerSprite.color = Color.white;
    }

    private bool HasRequiredItems(CoffeeItem preferredItem)
    {
        ItemSlot itemSlot = playerInv.slots.Find(slot => slot.item != null && slot.item.Name == preferredItem.Name &&
        slot.item is CoffeeItem coffee &&
        coffee.sugarSpoons == preferredItem.sugarSpoons &&
        coffee.milk == preferredItem.milk
        );
        return itemSlot != null && itemSlot.count >= 1;
    }

    private void RemoveItemFromInventory(Item preferredItem)
    {
        ItemSlot itemSlot = playerInv.slots.Find(slot => slot.item != null && slot.item.Name == preferredItem.Name);

        if (itemSlot != null)
        {
            itemSlot.count--;
            if (itemSlot.count <= 0)
            {
                itemSlot.item = null;
                itemSlot.count = 0;
            }
        }
    }
}
