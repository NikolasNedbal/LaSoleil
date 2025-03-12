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

    [SerializeField]
    private Item CoffeeBeans;
    [SerializeField]
    private Item Sugar;
    [SerializeField]
    private Item Milk;

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
        if (playerActive && Input.GetKeyDown(KeyCode.F) && CheckItemInInv(CoffeeBeans))
        {
            GameManager.Instance.invContainer.Remove(CoffeeBeans, 1);
            ct.ResetSlider();
            a.SetActive(!a.activeSelf);
            if (a.activeInHierarchy && !isCreating) 
            {
                isCreating = true;
                CreateNewCoffee();
            }
        }
        else
        {
            Debug.Log("You don't have beans");
        }
    }

    bool CheckItemInInv(Item item)
    {
        if(GameManager.Instance.invContainer.HasItem(item.Name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CreateNewCoffee()
    {
        currentCoffee = ScriptableObject.CreateInstance<CoffeeItem>();

        currentCoffee.Name = "Coffee";
        currentCoffee.stackable = false;
        currentCoffee.icon = icon;
        currentCoffee.sugarSpoons = 0;
        currentCoffee.milk = false;
    }

    public void AddMilk()
    {
        if (CheckItemInInv(Milk))
        {
            currentCoffee.milk = true;
            Debug.Log("Milk added");
            GameManager.Instance.invContainer.Remove(Milk, 1);
        }
        else
        {
            Debug.Log("Don't have milk");
        }
        
    }

    public void AddSugar()
    {
        if (CheckItemInInv(Sugar))
        {
            currentCoffee.sugarSpoons++;
            Debug.Log($"Sugar added, total: {currentCoffee.sugarSpoons}");
            GameManager.Instance.invContainer.Remove(Sugar, 1);
        }
        else
        {
            Debug.Log("Don't have sugar");
        }

    }

    public void TrashItem()
    {
        currentCoffee = null;
        Debug.Log("Coffee trashed.");
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
            Debug.Log("No coffee");
            return;
        }

        playerInventory.Add(currentCoffee, 1);
        Debug.Log("Coffee added to inventory");
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
