using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI txt;

    int indx;

    public void SetIndex(int index)
    {
        indx = index;
    }

    public void Set(ItemSlot slot)
    {
        icon.sprite = slot.item.icon;

        if (slot.item.stackable)
        {
            txt.gameObject.SetActive(true);
            txt.text = slot.count.ToString();
        }
        else
        {
            txt.gameObject.SetActive(false);
        }
    }

    public void ThrashIsntDoneWithYou()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        txt.gameObject.SetActive(false);
    }
}
