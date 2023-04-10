using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private Transform _playerTrm; //이건 나중에 찾아오는 형식으로 변경해야 함.
    public Transform PlayerTrm => _playerTrm;

    [SerializeField] private Transform spawnPointParent;

    private List<Transform> spawnPointList;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        TimeController.Instance = transform.AddComponent<TimeController>();

        MakePool();
        spawnPointList = new List<Transform>();
        spawnPointParent.GetComponentsInChildren<Transform>(spawnPointList);
        spawnPointList.RemoveAt(0);
    }


    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform); //풀매니저 만들어주고
        _poolingList.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount));  
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        float curTime = 0;
        while (true)
        {
            curTime += Time.deltaTime;
            if(curTime >= 5f)
            {
                curTime = 0;
                int idx = Random.Range(0, spawnPointList.Count);

                EnemyBrain enemy = PoolManager.Instance.Pop("EnemyGrowler") as EnemyBrain;
                enemy.transform.position = spawnPointList[idx].position;
                enemy.ShowEnemy();
            }
            yield return null;
        }

    }
}
