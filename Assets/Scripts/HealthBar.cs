using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform _barTransform;

    private void Awake()
    {
        _barTransform = transform.Find("Bar");
    }

    private void Start()
    {
        healthSystem.OnDamaged += OnDamaged;
        UpdateBar();
        UpdateBarVisibility();
    }

    private void OnDamaged(object sender, EventArgs e)
    {
        UpdateBar();
        UpdateBarVisibility();
    }

    private void UpdateBar()
    {
        _barTransform.localScale = new Vector3(healthSystem.GetHealthAmountNormalized(), 1, 1);
    }

    private void UpdateBarVisibility()
    {
        if (healthSystem.IsFullHealth())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    
}
