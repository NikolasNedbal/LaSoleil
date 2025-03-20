using NUnit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] UnityEngine.UI.Image icon;
    [SerializeField] TextMeshProUGUI txt;

    [SerializeField] TextMeshProUGUI selectedItemTxt;
    [SerializeField] Item item;

    public static InventoryButton selectedButton;

    public int indx { get; private set; }
    private void Awake()
    {
        if (icon == null) Debug.LogError("Icon is not assigned in InventoryButton.");
        if (txt == null) Debug.LogError("Text is not assigned in InventoryButton.");

        selectedItemTxt = GameObject.FindGameObjectWithTag("A").GetComponent<TextMeshProUGUI>();
        if (selectedItemTxt == null) Debug.LogError("selectedItemTxt is not assigned.");
    }

    public void OnClick()
    {
        selectedItemTxt.text = item != null ? item.name : "No Item";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void SwapSlots(InventoryButton button1, InventoryButton button2)
    {
        if (button1.item == null || button2.item == null)
        {
            Debug.Log("Cant swap empty slot");
            return;
        }

        ItemSlot tempSlot = new ItemSlot();
        tempSlot.item = button1.item;
        tempSlot.count = button1.indx >= 0 ? GameManager.Instance.invContainer.slots[button1.indx].count : 0;

        GameManager.Instance.invContainer.slots[button1.indx].item = button2.item;
        GameManager.Instance.invContainer.slots[button2.indx].item = tempSlot.item;

        button1.Set(GameManager.Instance.invContainer.slots[button1.indx]);
        button2.Set(GameManager.Instance.invContainer.slots[button2.indx]);
    }

    public void ThrashIsntDoneWithYou()
    {
        icon.sprite = null;
        icon.gameObject.SetActive(false);
        txt.gameObject.SetActive(false);
    }

    public void MoveToAnotherContainer(ItemContainer fromContainer, ItemContainer toContainer, int fromIndex, int toIndex)
    {
        ItemSlot firstSlot = fromContainer.slots[fromIndex];
        ItemSlot secondSlot = toContainer.slots[toIndex];
        if (fromContainer == toContainer && fromIndex == toIndex)
            return;

        if (firstSlot.item == null) return;

        if (secondSlot.item == null)
        {
            secondSlot.item = firstSlot.item;
            secondSlot.count = firstSlot.count;
            firstSlot.item = null;
            firstSlot.count = 0;
        }
        else if (secondSlot.item == firstSlot.item && secondSlot.item.stackable)
        {
            secondSlot.count += firstSlot.count;
            firstSlot.item = null;
            firstSlot.count = 0;
        }
        else
        {
            Item tempItem = secondSlot.item;
            int tempCount = secondSlot.count;

            secondSlot.item = firstSlot.item;
            secondSlot.count = firstSlot.count;

            firstSlot.item = tempItem;
            firstSlot.count = tempCount;
        }
        GameManager.Instance.invPanel.Show();
        GameManager.Instance.storagePanel.Show();
    }

    public void SetIndex(int index)
    {
        indx = index;
    }

    public void Set(ItemSlot slot)
    {
        if (slot.item != null)
        {
            icon.sprite = slot.item.icon;
            icon.gameObject.SetActive(true);

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
        else
        {
            icon.sprite = null;
            icon.gameObject.SetActive(false);
            txt.gameObject.SetActive(false);
            item = null;
        }
    }

    public void OnRightClick()
    {
        if (selectedButton == null)
        {
            selectedButton = this;
        }
        else
        {
            bool selectedInInv = GameManager.Instance.invPanel.buttons.Contains(selectedButton);
            bool selectedInStorage = GameManager.Instance.storagePanel.buttons.Contains(selectedButton);
            bool selectedInFer = GameManager.Instance.ferPanel.buttons.Contains(selectedButton);
            bool selectedInGramophone = GameManager.Instance.grPanel.buttons.Contains(selectedButton);

            bool thisInInv = GameManager.Instance.invPanel.buttons.Contains(this);
            bool thisInStorage = GameManager.Instance.storagePanel.buttons.Contains(this);
            bool thisInFer = GameManager.Instance.ferPanel.buttons.Contains(this);
            bool thisInGramophone = GameManager.Instance.grPanel.buttons.Contains(this);

            ItemContainer fromContainer = null, toContainer = null;
            int fromIndex = selectedButton.indx;
            int toIndex = this.indx;

            if (selectedInInv && thisInInv)
            {
                GameManager.Instance.invContainer.SwapItems(fromIndex, toIndex);
            }
            else if ((selectedInInv && thisInStorage) || (selectedInStorage && thisInInv))
            {
                fromContainer = selectedInInv ? GameManager.Instance.invContainer : GameManager.Instance.storageContainer;
                toContainer = selectedInStorage ? GameManager.Instance.invContainer : GameManager.Instance.storageContainer;
                MoveToAnotherContainer(fromContainer, toContainer, fromIndex, toIndex);
            }
            else if ((selectedInInv && thisInFer) || (selectedInFer && thisInInv))
            {
                fromContainer = selectedInInv ? GameManager.Instance.invContainer : GameManager.Instance.ferContainer;
                toContainer = selectedInFer ? GameManager.Instance.invContainer : GameManager.Instance.ferContainer;
                MoveToAnotherContainer(fromContainer, toContainer, fromIndex, toIndex);
            }
            else if ((selectedInInv && thisInGramophone) || (selectedInGramophone && thisInInv))
            {
                fromContainer = selectedInInv ? GameManager.Instance.invContainer : GameManager.Instance.grContainer;
                toContainer = selectedInGramophone ? GameManager.Instance.invContainer : GameManager.Instance.grContainer;
                MoveToAnotherContainer(fromContainer, toContainer, fromIndex, toIndex);
            }
            else
            {
                Debug.Log("nn");
            }

            RefreshUI();
            selectedButton = null;
        }
    }

    private void RefreshUI()
    {
        GameManager.Instance.invPanel.Show();
        GameManager.Instance.storagePanel.Show();
        GameManager.Instance.ferPanel.Show();
        GameManager.Instance.grPanel.Show();
    }
}
