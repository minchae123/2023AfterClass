using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemData[] items;

    private void Start()
    {
        items = new ItemData[3];

        items[0] = Resources.Load<ItemData>("ItemData");
        items[1] = Resources.Load<ItemData>("ArmorData");
        items[2] = Resources.Load<ItemData>("ConsumableData");

        foreach(ItemData item in items)
        {
            Debug.Log($"æ∆¿Ã≈€ : {item.ItemName}");
        }
    }
}
