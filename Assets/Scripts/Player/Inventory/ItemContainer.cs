using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Container")]
public class ItemContainer : ScriptableObject
{
    public List<ItemSlot> slots = new List<ItemSlot>();

    public void Add(Item item, int count = 1)
    {
        if(item.stackable)
        {
            ItemSlot itemSlot = slots.Find(x => x.item == item);
            if(itemSlot != null)
            {
                itemSlot.count += count;
            }
            else
            {
                itemSlot = slots.Find(x => x.item == null);
                if(itemSlot != null)
                {
                    itemSlot.item = item;
                    itemSlot.count = count;
                }
            }
        }
        else
        {
            ItemSlot itemSlot = slots.Find(x => x.item == null);
            if(itemSlot != null)
            {
                itemSlot.item = item;
            }
        }
    }
}

[Serializable]
public class ItemSlot
{
    public Item item;
    public int count;
}
