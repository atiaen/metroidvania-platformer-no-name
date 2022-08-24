using System;
using System.Collections.Generic;
using UnityEngine;


public class PickupItem : MonoBehaviour
{
    public Item item;
    public Collider2D collider;
    public string pickableByTag;
    public static event Action<int> coinsEvent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(pickableByTag))
        {
            Inventory playerInv = other.gameObject.GetComponent<InventoryHolder>().inventory;
            if (item.itemType == Common.ItemType.Coins)
            {
                playerInv.coins++;
                coinsEvent?.Invoke(playerInv.coins);

            }
            else
            {
                playerInv.AddItem(item);
            }
            gameObject.SetActive(false);
        }
    }


}
