using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIHandler : MonoBehaviour
{

    public TMP_Text coinsText;
    public Image selectedItemImage;
    void Awake()
    {
        PickupItem.coinsEvent += UpdateCoins;
        InventoryHolder.coinsEventHolder += UpdateCoins;

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
        Debug.Log(amount);
        coinsText.text = amount.ToString();
    }
}
