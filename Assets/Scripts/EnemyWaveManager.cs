using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyWaveManager : MonoBehaviour
{
   public event EventHandler OnWaveNumberChanged;
   
   [SerializeField] private GameObject HQ;
   [SerializeField] private List<Transform> spawnPositionTransforms;
   [SerializeField] private Transform nextWaveSpawnPositionTransform;
   private float _nextWaveSpawnTimer;
   private float _nextEnemySpawnTimer;
   private int _remainingEnemySpawnAmount;
   private Vector3 _spawnPos;
   private State _state;
   private Building _HQBuilding;
   private bool _IsHQAlive;
   private int _waveNumber;

   public enum State
   {
      WaitingToSpawnNextWave,
      SpawningWave,
   }

   private void Awake()
   {
      _IsHQAlive = true;
   }

   private void Start()
   {
      _state = State.WaitingToSpawnNextWave;
      _nextWaveSpawnTimer = 3f;
      _HQBuilding = HQ.gameObject.GetComponent<Building>();
      _HQBuilding.OnHQDestroyed += OnHQDestroyed;
      _spawnPos = spawnPositionTransforms[Random.Range(0, spawnPositionTransforms.Count)].position;
      nextWaveSpawnPositionTransform.position = _spawnPos;
   }

   private void OnHQDestroyed(object sender, EventArgs e)
   {
      _IsHQAlive = false;
   }

   private void Update()
   {
      if (_IsHQAlive)
      {
         switch (_state)
         {
            case State.WaitingToSpawnNextWave:
               _nextWaveSpawnTimer -= Time.deltaTime;
               if (_nextWaveSpawnTimer < 0f)
               {
                  SpwanWave();
               }
               break;
            case State.SpawningWave:
               if (_remainingEnemySpawnAmount > 0)
               {
                  _nextEnemySpawnTimer -= Time.deltaTime;
                  if (_nextEnemySpawnTimer < 0f)
                  {
                     _nextEnemySpawnTimer = Random.Range(0f, .2f);
                     Enemy.Create(_spawnPos + UtilsClass.GetRandomDirection() * Random.Range(0f, 10f));
                     _remainingEnemySpawnAmount -= 1;

                     if (_remainingEnemySpawnAmount <= 0)
                     {
                        _state = State.WaitingToSpawnNextWave;
                        _spawnPos = spawnPositionTransforms[Random.Range(0, spawnPositionTransforms.Count)].position;
                        nextWaveSpawnPositionTransform.position = _spawnPos;
                        _nextWaveSpawnTimer = 10f;
                     }
                  }
               }
               break;
         }
      }

   }

   private void SpwanWave()
   {
      _remainingEnemySpawnAmount = 5 + 3 * _waveNumber;
      _state = State.SpawningWave;
      _waveNumber += 1;
      OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
   }

   public int GetWaveNumber()
   {
      return _waveNumber;
   }

   public float GetNextWaveSpawnTimer()
   {
      return _nextWaveSpawnTimer;
   }

   public Vector3 GetSpawnPosition()
   {
      return _spawnPos;
   }
}
