using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ItemType : MonoBehaviour
{
    public ItemAbility[] abillty;

    public int item_id = -1;
    public string item_name = "";

    public ItemType()
    {
        item_id = -1;
        item_name = "";
    }

    public ItemType(ItemObj itemObj)
    {
        item_id = itemObj.itemData.item_id;
        item_name = itemObj.name;
        abillty = new ItemAbility[itemObj.itemData.abillty.Length];

        for(int i = 0; i < abillty.Length; i++)
        {
            abillty[i] = new ItemAbility(itemObj.itemData.abillty[i].Min, itemObj.itemData.abillty[i].Max)
            {
                characterStack = itemObj.itemData.abillty[i].characterStack
            };
        }
    }
}
