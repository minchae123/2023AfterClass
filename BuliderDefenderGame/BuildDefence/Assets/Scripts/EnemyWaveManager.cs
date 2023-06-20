using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    private enum State
    {
        Waiting,
        SpawnWave,
    }

    [SerializeField] private List<Transform> spawnPositionList;
    [SerializeField] private Transform nextSpawnWavePosition;

    private State state;
    private int waveNumber = 1;

    private float waveSpawnTimer;
    private float enemySpawnTimer;
    private int remainingEnemySpawnAmount;
    Vector3 spawnPos;

    private void Start() {
        spawnPos = spawnPositionList[Random.Range(0, spawnPositionList.Count)].position;    
        state = State.Waiting;
        waveSpawnTimer = 3;
        nextSpawnWavePosition.position = spawnPos;
    }

    private void Update() {
        switch(state)
        {
            case State.Waiting:
                waveSpawnTimer -= Time.deltaTime;
                if(waveSpawnTimer < 0)
                {
                    SpawnWave();
                    state = State.SpawnWave;
                    waveNumber++;
                }
                break;

            case State.SpawnWave:
                if(remainingEnemySpawnAmount > 0)
                {
                    enemySpawnTimer -= Time.deltaTime;
                    if(enemySpawnTimer < 0)
                    {
                        enemySpawnTimer = Random.Range(0, 0.2f);
                        Enemy.Create(spawnPos + UtilsClass.GetRandomDir() * Random.Range(0,10));
                        remainingEnemySpawnAmount--;
                        if(remainingEnemySpawnAmount <= 0)
                        {
                            state = State.Waiting;
                            spawnPos = spawnPositionList[Random.Range(0, spawnPositionList.Count)].position;
                            nextSpawnWavePosition.position = spawnPos;
                        }
                    }
                }
                break;  
        }
    }

    private void SpawnWave()
    {
        waveSpawnTimer = 10;
        remainingEnemySpawnAmount = 5 + waveNumber * 3;
    }

    public int GetWaveNumver()
    {
        return waveNumber;
    }

    public float GetNextSpawnerTimer()
    {
        return waveSpawnTimer;
    }
}