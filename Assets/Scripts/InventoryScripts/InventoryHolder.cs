using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    public Inventory inventory;

    public Item selectedItem;
    public static event Action<int> coinsEventHolder;


    // Start is called before the first frame update
    void Start()
    {
        coinsEventHolder?.Invoke(inventory.coins);
        if (inventory.coins >= inventory.maxCoins)
        {
            inventory.coins = inventory.maxCoins;
        }
        if (inventory.Items.Count != 0)
        {
            selectedItem = inventory.Items[0];
        }
        else
        {
            selectedItem = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.coins >= inventory.maxCoins)
        {
            inventory.coins = inventory.maxCoins;
        }

    }
}
