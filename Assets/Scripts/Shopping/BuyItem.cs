using UnityEngine;

public class BuyItem : MonoBehaviour
{
    [SerializeField]
    private Item itemInStore;
    private ItemContainer inv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inv = GameManager.Instance.invContainer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onButtonClick()
    {
        if (GameManager.Instance.money >= itemInStore.price)
        {
            GameManager.Instance.RemoveMoney(itemInStore.price);
            inv.Add(itemInStore, 1);
            
        }
    }
}
