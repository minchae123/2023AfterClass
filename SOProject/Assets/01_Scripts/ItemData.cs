using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    Weapon, Armor, Consumable
}

[CreateAssetMenu(fileName ="ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    [SerializeField] private ItemType itemType;
    public ItemType ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }

    [SerializeField] private Sprite itemIcon;
    public Sprite ItemIcon
    {
        get { return itemIcon; }
        set { itemIcon = value; }
    }

    [SerializeField] private int itemPrice;
    public int ItemPrice
    {
        get { return itemPrice; }   
        set { itemPrice = value; }
    }
}
