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
            Debug.LogWarning("Cannot swap with an empty slot.");
            return;
        }

        // Temporary item slot to hold the item from button1
        ItemSlot tempSlot = new ItemSlot();
        tempSlot.item = button1.item;
        tempSlot.count = button1.indx >= 0 ? GameManager.Instance.invContainer.slots[button1.indx].count : 0;

        // Swap logic for the item slots
        GameManager.Instance.invContainer.slots[button1.indx].item = button2.item;
        GameManager.Instance.invContainer.slots[button2.indx].item = tempSlot.item;

        // Refresh UI after swap
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
        ItemSlot sourceSlot = fromContainer.slots[fromIndex];
        ItemSlot targetSlot = toContainer.slots[toIndex];

        // Prevent moving if both slots are the same
        if (fromContainer == toContainer && fromIndex == toIndex)
            return;

        if (sourceSlot.item == null) return; // Nothing to move

        // If target slot is empty, move the item
        if (targetSlot.item == null)
        {
            targetSlot.item = sourceSlot.item;
            targetSlot.count = sourceSlot.count;
            sourceSlot.item = null;
            sourceSlot.count = 0;
        }
        // If target slot has the same item and is stackable, combine them
        else if (targetSlot.item == sourceSlot.item && targetSlot.item.stackable)
        {
            targetSlot.count += sourceSlot.count;
            sourceSlot.item = null;
            sourceSlot.count = 0;
        }
        // If target slot has a different item, swap the slots
        else
        {
            Item tempItem = targetSlot.item;
            int tempCount = targetSlot.count;

            targetSlot.item = sourceSlot.item;
            targetSlot.count = sourceSlot.count;

            sourceSlot.item = tempItem;
            sourceSlot.count = tempCount;
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
            // Clear the button visuals for empty slots
            icon.sprite = null;
            icon.gameObject.SetActive(false);
            txt.gameObject.SetActive(false);
            item = null; // Also clear item reference
        }
    }

    public void OnRightClick()
    {
        if (selectedButton == null)
        {
            selectedButton = this; // Select the first button
            Debug.Log($"Selected button index: {selectedButton.indx}");
        }
        else
        {
            Debug.Log($"Target button index: {this.indx}");

            // Determine which containers the buttons belong to
            ItemContainer container = null;

            // Check if both buttons are part of the player's inventory
            if (GameManager.Instance.invPanel.buttons.Contains(selectedButton) &&
                GameManager.Instance.invPanel.buttons.Contains(this))
            {
                container = GameManager.Instance.invContainer; // Same container for inventory
            }

            // Swap within the same container
            if (container != null)
            {
                container.SwapItems(selectedButton.indx, this.indx);

                // Refresh the inventory UI
                GameManager.Instance.invPanel.Show();
            }
            else
            {
                // Handle moving between containers (Inventory <-> Storage)
                ItemContainer fromContainer, toContainer;
                int fromIndex = selectedButton.indx;
                int toIndex = this.indx;

                if (GameManager.Instance.invPanel.buttons.Contains(selectedButton))
                {
                    fromContainer = GameManager.Instance.invContainer; // Inventory → Storage
                    toContainer = GameManager.Instance.storageContainer;
                }
                else
                {
                    fromContainer = GameManager.Instance.storageContainer; // Storage → Inventory
                    toContainer = GameManager.Instance.invContainer;
                }

                // Move items between different containers
                MoveToAnotherContainer(fromContainer, toContainer, fromIndex, toIndex);
            }

            // Clear selected button
            selectedButton = null;
        }
    }
}
