using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Transform _targetTransform;
    private Rigidbody2D _rigidbody;
    private HealthSystem _healthSystem;
    private float _lookForTargetTimer;
    private float _lookForTargetTimerMax = 0.2f;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (BuildingManager.Instance.GetHQBuilding() != null)
        {
            _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }

        _healthSystem = GetComponent<HealthSystem>();
        _healthSystem.OnDied += OnDied;
        
        _lookForTargetTimer = Random.Range(0f, _lookForTargetTimerMax);
        
        
    }

    private void OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        
        HandleMovement();
        HandleTargeting();
       
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var building = col.gameObject.GetComponent<Building>();
        if (building != null)
        {
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            Destroy(gameObject);
        }
    }

    private void HandleMovement()
    {
        if (_targetTransform != null)
        {
            Vector3 moveDir = (_targetTransform.position - transform.position).normalized;
            float moveSpeed = 6f;
            _rigidbody.velocity = moveDir * moveSpeed;
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
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
        float targetMaxRadius = 10f;
        Collider2D[] colliderArray =  Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider in colliderArray)
        {
            Building building = collider.GetComponent<Building>();
            if (building != null)
            {
                if (_targetTransform == null)
                {
                    _targetTransform = building.transform;
                }
                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) <
                        Vector3.Distance(transform.position, _targetTransform.position))
                    {
                        //Found closer target
                        _targetTransform = building.transform;
                    }
                }
            }

            if (_targetTransform == null)
            {
                if (BuildingManager.Instance.GetHQBuilding() != null)
                {
                    _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
                }
            }
        }
    }

    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTransform =  Instantiate(pfEnemy, position, Quaternion.identity);

        return enemyTransform.GetComponent<Enemy>();
    }
}
