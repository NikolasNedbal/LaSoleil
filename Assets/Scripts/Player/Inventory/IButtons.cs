using System;
using TMPro;
using UnityEngine;

public class Buttons : MonoBehaviour 
{
    [SerializeField] UnityEngine.UI.Image icon;
    [SerializeField] TextMeshProUGUI txt;
    
    [SerializeField] TextMeshProUGUI selectedItemTxt;
    [SerializeField] Item item;

    public static Buttons selectedButton;

    int indx;
    public void OnClick()
    {
        selectedItemTxt.text = item != null ? item.name : "No Item";
    }
    public virtual void OnRightClick()
    {

        if (selectedButton == null)
        {

        }
        else
        {

        }
    }
    public void SetIndex(int index)
    {
        indx = index;
    }
    public void ThrashIsntDoneWithYou()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        txt.gameObject.SetActive(false);
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

        item = slot.item;
    }
}
