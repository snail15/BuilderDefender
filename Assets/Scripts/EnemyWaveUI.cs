using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyWaveUI : MonoBehaviour
{
    [SerializeField] private EnemyWaveManager enemyWaveManager;
    private TextMeshProUGUI _waveNumberText;
    private TextMeshProUGUI _waveMessageText;
    private RectTransform _enemyWaveSpawnPositionIndicator;
    private Camera _camera;
    private RectTransform _enemyClosestPositionIndicator;

    private void Awake()
    {
        _camera = Camera.main;
        _waveNumberText = transform.Find("WaveNumberText").GetComponent<TextMeshProUGUI>();
        _waveMessageText = transform.Find("WaveMessageText").GetComponent<TextMeshProUGUI>();
        _enemyWaveSpawnPositionIndicator =
            transform.Find("EnemyWaveSpawnPositionIndicator").GetComponent<RectTransform>();
        _enemyClosestPositionIndicator = transform.Find("EnemyClosestPositionIndicator").GetComponent<RectTransform>();
    }

    private void Start()
    {
        enemyWaveManager.OnWaveNumberChanged += OnWaveNumberChanged;
        SetText(_waveNumberText, "Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void OnWaveNumberChanged(object sender, EventArgs e)
    {
        SetText(_waveNumberText, "Wave " + enemyWaveManager.GetWaveNumber());
    }

    private void Update()
    {
        HandleNextWaveTimeIndicator();
        
        HandleEnemyWaveSpawnPositionIndicator();
        
        HandleEnemyClosestPositionIndicator();
       
        
    }

    private void HandleNextWaveTimeIndicator()
    {
        float nextWaveSpawnTimer = enemyWaveManager.GetNextWaveSpawnTimer();
        if (nextWaveSpawnTimer <= 0f)
        {
            SetText(_waveMessageText, "");
        }
        else
        {
            SetText(_waveMessageText, "Next Wave in " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void HandleEnemyWaveSpawnPositionIndicator()
    {
       

        Vector3 dirToNextSpawnPos =  (enemyWaveManager.GetSpawnPosition() - _camera.transform.position).normalized;
        _enemyWaveSpawnPositionIndicator.anchoredPosition = dirToNextSpawnPos * 300f;
        _enemyWaveSpawnPositionIndicator.eulerAngles =
            new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToNextSpawnPos));

        float distanceToNextSpawnPos =
            Vector3.Distance(enemyWaveManager.GetSpawnPosition(), _camera.transform.position);
        _enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPos > _camera.orthographicSize * 1.5);
    }

    private void HandleEnemyClosestPositionIndicator()
    {
        float targetMaxRadius = 9999f;
        Collider2D[] colliderArray =  Physics2D.OverlapCircleAll(_camera.transform.position, targetMaxRadius);
        Enemy targetEnemy = null;
        
        foreach (Collider2D collider in colliderArray)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (targetEnemy == null)
                {
                    targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, targetEnemy.transform.position))
                    {
                        //Found closer target
                        targetEnemy = enemy;
                    }
                }
            }
        }


        if (targetEnemy != null)
        {
            Vector3 dirToClosestEnemy =  (targetEnemy.transform.position - _camera.transform.position).normalized;
            _enemyClosestPositionIndicator.anchoredPosition = dirToClosestEnemy * 250f;
            _enemyClosestPositionIndicator.eulerAngles =
                new Vector3(0, 0, UtilsClass.GetAngleFromVector(dirToClosestEnemy));

            float distanceToClosestEnemy =
                Vector3.Distance(targetEnemy.transform.position, _camera.transform.position);
            _enemyClosestPositionIndicator.gameObject.SetActive(distanceToClosestEnemy > _camera.orthographicSize * 1.5);
        }
        else
        {
            _enemyClosestPositionIndicator.gameObject.SetActive(false);
        }
    }

    private void SetText(TextMeshProUGUI text, string message)
    {
        text.SetText(message);
    }
    
}
