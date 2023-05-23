using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

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
    }

    private void LogResource()
    {
        foreach(ResourceTypeSO resourceType in resourceAmountDic.Keys)
        {
            Debug.Log(resourceType.nameString + " : " + resourceAmountDic[resourceType]);
        }
    }

    public int GetResourceAmount(ResourceTypeSO resourceType){
        return resourceAmountDic[resourceType];
    }
}
