using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EnemyWaveUI : MonoBehaviour
{
    private EnemyWaveManager enemyWaveManager;

    private TextMeshProUGUI waveNumberText;
    private TextMeshProUGUI waveMessageText;

    private RectTransform enemyWaveSpawnPositionIndicator;
    private RectTransform enemyClosestPositionIndicator;

    private Camera mainCamera;

    private void Awake()
    {
        waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();

        enemyWaveSpawnPositionIndicator = transform.Find("EnemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        enemyClosestPositionIndicator = transform.Find("EnemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        enemyWaveManager = FindObjectOfType<EnemyWaveManager>();
        enemyWaveManager.OnWaveNumberChanged -= EnemyWaveManager_OnWaveManager;
        enemyWaveManager.OnWaveNumberChanged += EnemyWaveManager_OnWaveManager;
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleNextWaveMessage();
        HandleEnemyClosestPositionIndicator();
        HandleEnemyWaveSpawnPositionIndicator();

    }

    private void HandleNextWaveMessage()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetWaveMessageText("");
        }
        else
        {
            SetWaveMessageText($"Next Wave in {nextWaveSpawnTimer.ToString("F1")}s");
        }
    }

    private void HandleEnemyClosestPositionIndicator()
    {
        float targetMaxRadius = 20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        Enemy targetEnemy = null;

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy != null)
                {
                    // 가장 가까운 적
                    Vector3 dirToClosestEnemy = (targetEnemy.transform.position - mainCamera.transform.position).normalized;

                    enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
                    enemyClosestPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

                    float distanceToClosestEnemy = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
                    enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > mainCamera.orthographicSize * 1.5f);
                }
                else
                {
                    enemyClosestPositionIndicator.gameObject.SetActive(false);
                }
            }
        }
    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
        // 적 스폰 위치
        Vector3 dirToNextSpawnPos = (enemyWaveManager.GetSpawnPosition() - mainCamera.transform.position).normalized;

        enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPos * 300f;
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPos));

        float distanceToNextSpawnPos = Vector3.Distance(enemyWaveManager.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPos > mainCamera.orthographicSize * 1.5f);
    }

    private void EnemyWaveManager_OnWaveManager(object sender, System.EventArgs e)
    {
        SetWaveNumberText($"Wave {enemyWaveManager.GetWaverNumber()}");
    }

    private void SetWaveNumberText(string text)
    {
        waveNumberText.SetText(text);
    }
    private void SetWaveMessageText(string message)
    {
        waveMessageText.SetText(message);
    }
}
