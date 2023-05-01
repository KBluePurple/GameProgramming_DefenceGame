using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
    public static EnemyWaveManager Instance { get; private set; }

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    private State state;
    private int waveNumber;

    [SerializeField]
    private Transform[] spawnPosTranform;
    [SerializeField]
    private Transform nextWaveSpawnPosTransform;

    private float nextWaveSpawnTimer = 0f;
    private int remainingEnemySpawnAmount;
    private Vector3 spawnPosition;

    public event EventHandler OnWaveNumberChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = State.WaitingToSpawnNextWave;
        spawnPosition = spawnPosTranform[Random.Range(0, spawnPosTranform.Length)].position;
        nextWaveSpawnPosTransform.position = spawnPosition;
        nextWaveSpawnTimer = 3f;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToSpawnNextWave:
                nextWaveSpawnTimer -= Time.deltaTime;

                if (nextWaveSpawnTimer < 0f)
                {
                    SpawnWave();
                }
                break;

            case State.SpawningWave:
                if (remainingEnemySpawnAmount > 0)
                {
                    nextWaveSpawnTimer -= Time.deltaTime;

                    if (nextWaveSpawnTimer < 0f)
                    {
                        nextWaveSpawnTimer = Random.Range(0f, 0.2f);
                        Enemy.Create(spawnPosition + UtilsClass.GetRandomDir() * Random.Range(0f, 10f));
                        remainingEnemySpawnAmount--;

                        if(remainingEnemySpawnAmount <= 0f)
                        {
                            state = State.WaitingToSpawnNextWave;
                            spawnPosition = spawnPosTranform[Random.Range(0, spawnPosTranform.Length)].position;
                            nextWaveSpawnPosTransform.position = spawnPosition;
                        }
                    }
                }
                break;
        }
    }

    private void SpawnWave()
    {
        spawnPosition = spawnPosTranform[Random.Range(0, spawnPosTranform.Length)].position;
        nextWaveSpawnPosTransform.position = spawnPosition;

        nextWaveSpawnTimer = 10f;
        remainingEnemySpawnAmount = 5 + 3 * waveNumber;

        state = State.SpawningWave;
        waveNumber++;
        OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetWaverNumber() => waveNumber;
    public float GetNextWaveSpawnTimer() => nextWaveSpawnTimer;
    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
