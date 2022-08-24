using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item;
    void Start()
    {
        if (item.isStackable && item.itemCount >= item.maxItemCount)
        {
            item.itemCount = item.maxItemCount;
        }

    }

    void OnEnable()
    {
        if (item.isStackable && item.itemCount >= item.maxItemCount)
        {
            item.itemCount = item.maxItemCount;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
