using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemData healthData;

    private void Start()
    {
        CreateNewItem("Health Potion", Resources.Load<Sprite>("HealthPotion"), 10);
        CreateNewItem("Mana Potion", Resources.Load<Sprite>("ManaPotion"), 10);
        CreateNewItem("Atk Potion", Resources.Load<Sprite>("AtkPotion"), 10);
    }

    private void CreateNewItem(string name, Sprite icon, int price)
    {
        // ItemData newItem = new ItemData(); // 인스턴스 생성

        ItemData newItem = ScriptableObject.CreateInstance<ItemData>();
        newItem.itemName = name;
        newItem.itemIcon = icon;
        newItem.itemPrice = price;

        string assetPath = "Assets/" + name + ".asset";

        UnityEditor.AssetDatabase.CreateAsset(newItem, assetPath);
        UnityEditor.AssetDatabase.SaveAssets();

        Debug.Log("ScriptableObject 에셋으로 저장되었습니다. 경로 : " + assetPath);

        //Debug.Log($"새로운 아이템 생성 : {newItem.itemName}");
        //Debug.Log($"아이템 아이콘 : {newItem.itemIcon.name}");
        //Debug.Log($"아이템 가격 : {newItem.itemPrice}");
    }
}
