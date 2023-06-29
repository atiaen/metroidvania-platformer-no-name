using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIHandler : MonoBehaviour
{

    public TMP_Text coinsText;
    public TMP_Text itemCountText;
    public Image selectedItemImage;
    void Awake()
    {
        PickupItem.coinsEvent += UpdateCoins;
        InventoryHolder.coinsEventHolder += UpdateCoins;
        InventoryHolder.changeIconEvent += ShowInvItem;

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCoins(int amount)
    {
        coinsText.text = amount.ToString();
    }

    public void ShowInvItem(Sprite itemImg, int itemCount)
    {
        itemCountText.enabled = true;
        itemCountText.text =  itemCount.ToString();
        selectedItemImage.sprite = itemImg;
        selectedItemImage.enabled = true;
    }
}
