using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    private enum State
    {
        Waiting,
        SpawnWave,
    }

    public event EventHandler OnWaveNumverChanged;

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
                    OnWaveNumverChanged?.Invoke(this, EventArgs.Empty);
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
                            waveSpawnTimer = 10;
                        }
                    }
                }
                break;  
        }
    }

    private void SpawnWave()
    {
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