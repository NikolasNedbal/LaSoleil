using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inv;
    [SerializeField] public List<InventoryButton> buttons;

    [SerializeField]
    private bool isStorage = false;

    void Start()
    {
        Show();
        SetIndex();
    }

    // Update is called once per frame
    void Update()
    {
        if(inv == null)
        {
            if (isStorage)
            {
                inv = GameManager.Instance.storageContainer;
            }
            else
            {
                inv = GameManager.Instance.invContainer;
            }
        }
    }

    private void OnEnable()
    {
        Show();
    }

    private void SetIndex()
    {
        for (int i = 0; i < inv.slots.Count; i++)
        {
            buttons[i].SetIndex(i);
        }
    }

    public void Show()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            // Reset and update button states
            buttons[i].ThrashIsntDoneWithYou();
            buttons[i].Set(inv.slots[i]);
        }
    }
}

