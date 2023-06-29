using System;
using DG.Tweening;
using UnityEngine;


public class PickupItem : MonoBehaviour
{
    public Item item;
    public string pickableByTag;
    // Rigidbody2D rigidbody;

    [SerializeField]
    float floatHeight;

    [SerializeField]
    float floatSpeed;
    public static event Action<int> coinsEvent;

    // Start is called before the first frame update
    void Start()
    {
        // rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // rigidbody
        // .DOMoveY(floatHeight, floatSpeed)
        // .SetEase(Ease.InOutSine)
        // .SetLoops(-1, LoopType.Yoyo);
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
