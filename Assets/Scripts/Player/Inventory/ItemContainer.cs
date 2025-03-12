using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots = new List<ItemSlot>();

    /*public void Add(Item item, int count = 1)
    {
        Debug.Log($"Adding item: {item.GetType()}");

        if(item == null)
        {
            Debug.Log("Item is null");
            return;
        }

        if (item.stackable)
        {
            //ItemSlot itemSlot = slots.Find(x => x.item == item);
            ItemSlot itemSlot = slots.Find(x => x.item != null && x.item.Name == item.Name);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            //ItemSlot itemSlot = slots.Find(x => x.item == null);
            ItemSlot itemSlot = slots.Find(x => x.item != null && x.item.Name == item.Name);
            if (itemSlot != null)
            {
                itemSlot.item = item;
                itemSlot.count = 1;
            }
        }
    }*/

    public void Add(Item item, int count = 1)
    {
        if (item == null)
        {
            Debug.LogError("item is null");
            return;
        }


        foreach (var slot in slots)
        {
            if (slot.item != null)
            {
                Debug.Log($"Slot contains: {slot.item.GetType()} - {slot.item.Name}");
            }
        }

        if (item.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if (itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if (itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if (itemSlot != null)
            {
                itemSlot.item = item;
                itemSlot.count = 1;
            }
        }
    }

    public void Remove(Item item, int count = 1)
    {
        ItemSlot itemSlot = slots.Find(x => x.item == item);
        if (itemSlot != null)
        {
            itemSlot.count -= count;
            if (itemSlot.count <= 0)
            {
                itemSlot.item = null;
                itemSlot.count = 0;
            }
        }
    }

    public bool HasItem(string itemName)
    {
        foreach (var slot in slots)
        {
            if (slot.item != null)
            {
                Debug.Log($"Found item in slot: {slot.item.Name}");
            }
        }
        return slots.Exists(slot => slot.item != null && slot.item.Name == itemName);
    }

    public void SwapItems(int index1, int index2)
    {
        ItemSlot temp = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = temp;
    }
}

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;
}
