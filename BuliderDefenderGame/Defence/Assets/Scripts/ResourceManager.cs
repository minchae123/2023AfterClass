using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    private Dictionary<ResourceTypeSO, int> resourceAmountDic;
    private ResourceTypeListSO resourceTypeList;

    private void Awake()
    {
        Instance = this;
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        resourceAmountDic = new Dictionary<ResourceTypeSO, int>();

        foreach(ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDic[resourceType] = 0;
        }
    }
   
    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDic[resourceType] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    
    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDic[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray)
    {
        foreach(ResourceAmount r in resourceAmountArray)
        {
            if(GetResourceAmount(r.resourceType) >= r.amount)
            {

            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public void SpendResource(ResourceAmount[] resourceAmountArray)
    {
        foreach(ResourceAmount r in resourceAmountArray)
        {
            resourceAmountDic[r.resourceType] -= r.amount;
        }
    }
}
