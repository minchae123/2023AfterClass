using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Items/ResourcesData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate; // 드랍확률
    public GameObject ItemPrefab; // 해당 아이템 프리팹 저장

    public ItemType itemType;

    [SerializeField]
    private int minAmount = 1, maxAmount = 5;

    public Color popupTextColor;
    public AudioClip useSound;

    public int GetAmount()
    {
        return Random.Range(minAmount, maxAmount);
    }
}
