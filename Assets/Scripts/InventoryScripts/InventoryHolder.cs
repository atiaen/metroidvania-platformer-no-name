using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public Inventory inventory;

    public static event Action<int> coinsEventHolder;

    public static event Action<float> healEvent;
    public static event Action<Sprite, int> changeIconEvent;

    int index;

    // Start is called before the first frame update
    void Start()
    {
        if (inventory.Items.Count > 0)
        {
            inventory.currSelectItem = inventory.Items[0];
            changeIconEvent?.Invoke(inventory.currSelectItem.itemIcon, inventory.currSelectItem.itemCount);
        }
        coinsEventHolder?.Invoke(inventory.coins);
        if (inventory.coins >= inventory.maxCoins)
        {
            inventory.coins = inventory.maxCoins;
        }
        if (inventory.Items.Count != 0 && inventory.currSelectItem == null)
        {
            inventory.currSelectItem = inventory.Items[0];
        }
        else
        {
            inventory.currSelectItem = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.coins >= inventory.maxCoins)
        {
            inventory.coins = inventory.maxCoins;
        }
        TraverseInv();
    }

    public void TraverseInv()
    {
        bool previous, next;
        previous = Input.GetButtonDown("Previous");
        next = Input.GetButtonDown("Next");
        // int index = inventory.Items.IndexOf(inventory.currSelectItem);
        // int capacity = inventory.Items.Count;

        if (previous)
        {
            PreviousItem();
        }
        if (next)
        {
            NextItem();
        }

        if (Input.GetButtonDown("Use Inventory Item") && inventory.currSelectItem)
        {
            if(inventory.currSelectItem.itemCount > 0 && inventory.currSelectItem.itemType == Common.ItemType.Potion)
            {
                if(inventory.currSelectItem.potionType == Common.PotionType.HealthPotion)
                {
                    inventory.currSelectItem.itemCount--;
                    changeIconEvent?.Invoke(inventory.currSelectItem.itemIcon, inventory.currSelectItem.itemCount);
                    healEvent?.Invoke(inventory.currSelectItem.healAmount);
                }

            }

        }
    }

    public void PreviousItem()
    {
        // Hide current model
        index--;
        int capacity = inventory.Items.Count;
        if (index < 0)
        {
            index = capacity - 1;
        }

        // Show previous model
        inventory.currSelectItem = inventory.Items[index];
        changeIconEvent?.Invoke(inventory.currSelectItem.itemIcon, inventory.currSelectItem.itemCount);
    }

    public void NextItem()
    {
        index++;
        int capacity = inventory.Items.Count;
        if (index > capacity - 1)
        {
            index = 0;
        }

        // Select next item and show it
        inventory.currSelectItem = inventory.Items[index];
        changeIconEvent?.Invoke(inventory.currSelectItem.itemIcon, inventory.currSelectItem.itemCount);
    }

}
