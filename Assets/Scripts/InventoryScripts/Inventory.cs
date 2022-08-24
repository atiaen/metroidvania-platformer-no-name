using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class Inventory : ScriptableObject
{
    public int coins;
    public int maxCoins;

    public Item currSelectItem;
    public List<Item> Items = new List<Item>();



    public void AddItem(Item item)
    {
        if (Items.Contains(item) && item.isStackable && item.itemType != Common.ItemType.Coins)
        {
            if (item.isItemMax() == true)
            {
                item.itemCount = item.maxItemCount;
            }
            else
            {
                item.itemCount++;
            }

        }
        else if (item.itemType == Common.ItemType.Coins)
        {
            coins++;

        }
        else
        {
            Items.Add(item);
            item.itemCount++;
        }
    }

    public void RemoveItem(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
        }
    }
}
