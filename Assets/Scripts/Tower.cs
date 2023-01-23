using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float shootTimerMax;
    private float _shootTimer;
    private Enemy _targetEnemy;
    private float _lookForTargetTimer;
    private float _lookForTargetTimerMax = 0.2f;
    private Vector3 _projectileSpawnPos;
    private void Awake()
    {
        _projectileSpawnPos = transform.Find("projectileSpawnPosition").position;
    }

    private void Update()
    {
        HandleTargeting();
        HandleShooting();
    }

    private void HandleShooting()
    {
        _shootTimer -= Time.deltaTime;
        if (_shootTimer <= 0f)
        {
            _shootTimer += shootTimerMax;
            if (_targetEnemy != null)
            {
                ArrowProjectile.Create(_projectileSpawnPos, _targetEnemy);
            }
        }
       
    }

    private void HandleTargeting()
    {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer < 0)
        {
            _lookForTargetTimer += _lookForTargetTimerMax;
            LookForTarget();
        }
    }
    
    private void LookForTarget()
    {
        float targetMaxRadius = 20f;
        Collider2D[] colliderArray =  Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider in colliderArray)
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (_targetEnemy == null)
                {
                    _targetEnemy = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) <
                        Vector3.Distance(transform.position, _targetEnemy.transform.position))
                    {
                        //Found closer target
                        _targetEnemy = enemy;
                    }
                }
            }
        }
    }
}
