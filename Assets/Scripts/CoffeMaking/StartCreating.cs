using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StartCreating : MonoBehaviour
{
    private bool playerActive = false;
    [SerializeField]
    private GameObject a;
    [SerializeField]
    private CreationTimer ct;

    public ItemContainer playerInventory;
    private CoffeeItem currentCoffee;

    [SerializeField]
    private Button pickUpBtn;
    [SerializeField]
    private Button addSugarBtn;
    [SerializeField]
    private Button addMilkBtn;

    public RectTransform[] btnRect;
    public RectTransform bgRect;

    public float moveInterval = 5f;

    public Sprite icon;
    private bool isCreating = false;

    void Start()
    {
        pickUpBtn.enabled = false;
        playerInventory = GameManager.Instance.invContainer;
    }

    void Update()
    {
        if (a.activeInHierarchy)
        {
            EnableBtn();
        }
        if (playerActive && Input.GetKeyDown(KeyCode.F))
        {
            ct.ResetSlider();
            a.SetActive(!a.activeSelf);
            if (a.activeInHierarchy && !isCreating) 
            {
                isCreating = true;
                CreateNewCoffee();
            }
        }
    }

    void CreateNewCoffee()
    {
        currentCoffee = ScriptableObject.CreateInstance<CoffeeItem>();
        currentCoffee.Name = "Latte";
        currentCoffee.stackable = false;
        currentCoffee.icon = icon;
        currentCoffee.sugarSpoons = 0;
        currentCoffee.milk = false;

    }

    public void AddMilk()
    {
        currentCoffee.milk = true;
        Debug.Log("Milk added to the coffee.");
    }

    public void AddSugar()
    {
        currentCoffee.sugarSpoons++;
        Debug.Log($"Sugar added. Total spoons: {currentCoffee.sugarSpoons}");
    }

    public void TrashItem()
    {
        currentCoffee = null;
        Debug.Log("Coffee item trashed.");
        CreateNewCoffee();
    }

    private void EnableBtn()
    {
        if (ct.timerEnded)
        {
            pickUpBtn.enabled = true;
            addMilkBtn.enabled = false;
            addSugarBtn.enabled = false;
        }

        if (!pickUpBtn.enabled)
        {
            addMilkBtn.enabled = true;
            addSugarBtn.enabled = true;
        }
    }

    public void PickUpItem()
    {
        isCreating = false;
        if (currentCoffee == null)
        {
            Debug.Log("No coffee item to add to inventory.");
            return;
        }

        playerInventory.Add(currentCoffee, 1);
        Debug.Log("Coffee item added to inventory.");
        pickUpBtn.enabled = false;
        a.SetActive(false);
    }

    void MoveButtonRandomly()
    {
        foreach (RectTransform buttonRect in btnRect)
        {
            MoveButton(buttonRect);
        }
    }

    void MoveButton(RectTransform buttonRect)
    {
        Vector2 parentSize = bgRect.rect.size;

        Vector2 buttonSize = buttonRect.rect.size;

        float randomX = Random.Range(-parentSize.x / 2 + buttonSize.x / 2, parentSize.x / 2 - buttonSize.x / 2);
        float randomY = Random.Range(-parentSize.y / 2 + buttonSize.y / 2, parentSize.y / 2 - buttonSize.y / 2);

        buttonRect.anchoredPosition = new Vector2(randomX, randomY);
    }

    void OnEnable()
    {
        InvokeRepeating("MoveButtonRandomly", moveInterval, 1f);
    }

    void OnDisable()
    {
        CancelInvoke("MoveButtonRandomly");
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
}
