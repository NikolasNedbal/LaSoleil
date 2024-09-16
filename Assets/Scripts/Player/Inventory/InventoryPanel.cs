using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField] ItemContainer inv;
    [SerializeField] List<InventoryButton> buttons;
    // Start is called before the first frame update
    void Start()
    {
        Show();
        SetIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void Show()
    {
        for (int i = 0; i < inv.slots.Count; i++)
        {
            if (inv.slots[i].item == null)
            {
                buttons[i].ThrashIsntDoneWithYou();
            }
            else 
            {
                buttons[i].Set(inv.slots[i]);
            }
        }
    }
}
