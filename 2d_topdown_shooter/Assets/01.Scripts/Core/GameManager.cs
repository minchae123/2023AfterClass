using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;
using Sequence = DG.Tweening.Sequence;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _poolingList;

    [SerializeField]
    private Transform _playerTrm; //이건 나중에 찾아오는 형식으로 변경해야 함.
    public Transform PlayerTrm => _playerTrm;

    [SerializeField] private Transform spawnPointParent;
    [SerializeField] private SpawnListSO spawnList;
    private float[] spawnWeights;

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

        spawnWeights = spawnList.spawnPairs.Select(s => s.spawnPercent).ToArray();
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
                // 시퀀스 줘서 4~6마리가 스폰포인트 반경 2 유닛 범위에서 순차적으로 나오게

                int cnt = Random.Range(2, 5);
                for(int i = 0; i < cnt; i++)
                {
                    int sIndex = GetRandomSpawnIndex();

                    EnemyBrain enemy = PoolManager.Instance.Pop(spawnList.spawnPairs[sIndex].prefab.name) as EnemyBrain;
                    Vector2 posOffset = Random.insideUnitCircle * 2;

                    enemy.transform.position = spawnPointList[idx].position + (Vector3)posOffset;
                    enemy.ShowEnemy();

                    float showTime = Random.Range(0.1f, 0.3f);
                    yield return new WaitForSeconds(showTime);
                }
            }
            yield return null;
        }
    }

    private int GetRandomSpawnIndex()
    {
        float sum = 0;
        for(int i = 0; i < spawnWeights.Length; i++)
        {
            sum += spawnWeights[i];
        }

        float randomValue = Random.Range(0f, sum);
        float tempSum = 0;

        for (int i = 0; i < spawnWeights.Length; i++)
        {
            if(randomValue >= tempSum && randomValue < tempSum + spawnWeights[i])
            {
                return i;
            }
        }
        return 0;
    }
}
