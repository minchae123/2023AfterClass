using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class ItemDropper : MonoBehaviour
{
    [SerializeField] private ItemDropTableSO dropTable;
    private float[] itemWeights;

    [SerializeField] private bool dropEffect = false;
    [SerializeField] private float dropPower = 2f;

    [SerializeField]
    [Range(0, 1)]
    private float dropChance;

    private void Start()
    {
        itemWeights = dropTable.DropList.Select(item => item.Rate).ToArray();        
    }

    public void DropItem()
    {
        float dropVariable = Random.value;
        if (dropVariable < dropChance)
        {
            int idx = GetRandomWeightIndex();
            ItemScript item = PoolManager.Instance.Pop(dropTable.DropList[idx].ItemPrefab.name) as ItemScript;

            item.transform.position = transform.position;

            if (dropEffect)
            {
                Vector3 offset = Random.insideUnitCircle * 1.5f;
                item.transform.DOJump(transform.position + offset, dropPower, 1, 0.35f);
            }
        }
    }

    private int GetRandomWeightIndex()
    {
        float sum = 0;
        for (int i = 0; i < itemWeights.Length; i++)
        {
            sum += itemWeights[i];

        }
        float randomValue = Random.Range(0, sum);
        float tempSum = 0;

        for(int i = 0; i < itemWeights.Length; i++)
        {
            if (randomValue >= tempSum && randomValue < tempSum + itemWeights[i])
                return i;
            else
                tempSum += itemWeights[i];
        }
        return 0;
    }
}
