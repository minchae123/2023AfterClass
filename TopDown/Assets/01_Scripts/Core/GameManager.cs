using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PoolingListSO poolingList;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;
        
        MakePool();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform); // 풀매니저 만들고
        poolingList.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount)); // 리스트에 p 로 모두 들어와서 foreach 도는거
    }
}
