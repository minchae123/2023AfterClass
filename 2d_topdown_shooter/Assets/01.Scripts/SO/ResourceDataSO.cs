using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Items/ResourcesData")]
public class ResourceDataSO : ScriptableObject
{
    public float Rate; // ���Ȯ��
    public GameObject ItemPrefab; // �ش� ������ ������ ����

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
