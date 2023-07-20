using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemData itemData;

    private void Start()
    {
        itemData = Resources.Load<ItemData>("ItemData");

        Debug.Log($"아이템 : {itemData.ItemName}");
        Debug.Log($"종류 : {itemData.ItemType}");
        Debug.Log($"가격 : {itemData.ItemPrice}");
        Debug.Log($"아이콘 : {itemData.ItemIcon.name}");
    }
}
