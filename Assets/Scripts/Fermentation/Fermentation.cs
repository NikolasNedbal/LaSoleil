using UnityEngine;
using UnityEngine.UI;

public class Fermentation : MonoBehaviour
{
    [SerializeField]
    private ItemContainer itemsToFer;
    private int day = 0;

    [SerializeField]
    private Item coffeeBeans;

    [SerializeField]
    private Button startBtn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isFerInProcess)
        {
            startBtn.enabled = false;
        }
        else
        {
            startBtn.enabled = true;
        }
    }

    public void FermentationProcess()
    {
        if (!GameManager.Instance.isFerInProcess)
        {
            GameManager.Instance.isFerInProcess = true;
            if (day == 0) day = GameManager.Instance.days;
            Debug.Log("Day==== " + day);
        }
        else
        {
            if (day != GameManager.Instance.days)
            {
                Debug.Log("Works");
                foreach (ItemSlot slot in itemsToFer.slots)
                {
                    if (slot.item != null && slot.item.Name == "CoffePlant")
                    {
                        slot.item = coffeeBeans;
                        Debug.Log("Works2");
                    }
                    else
                    {
                        Debug.Log("EmptySlotInFermentation");
                    }
                }
                GameManager.Instance.isFerInProcess = false;
            }
        }
    }
}
