using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        itemData = Resources.Load<ItemData>("ItemData");

        Debug.Log($"������ : {itemData.ItemName}");
        Debug.Log($"���� : {itemData.ItemType}");
        Debug.Log($"���� : {itemData.ItemPrice}");
        Debug.Log($"������ : {itemData.ItemIcon.name}");
    }
}
