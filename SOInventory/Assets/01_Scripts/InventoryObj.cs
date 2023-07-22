using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public enum InterfaceType
{
    Inventory,
    Equipment,
    QuickSlot,
    Box
}

[CreateAssetMenu(fileName ="New InventoryObject", menuName ="Inventory/InventoryObject")]
public class InventoryObj : ScriptableObject
{
    public ItemDBObj itemDBObj;
    public InterfaceType type;

    [SerializeField] private Inventory inventory = new Inventory();
    public InventorySlot[] inventorySlots => inventory.inventorySlots;

    public int GetEmptySlotCnt
    {
        get
        {
            int cnt = 0;
            foreach(InventorySlot slot in inventorySlots)
            {
                if(slot.itemType.item_id <= -1)
                {
                    cnt++;
                }
            }
            return cnt;
        }
    }

    public Action<ItemObj> OnUseItemObj;

    public InventorySlot SearchitemInven(ItemType item)
    {
        return inventorySlots.FirstOrDefault(i => i.itemType.item_id == item.item_id);
    }
}
