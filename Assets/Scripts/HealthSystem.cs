using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;
    public event EventHandler OnHealed;
    [SerializeField] private int healthAmountMax;
    private int _healthAmount;

    private void Awake()
    {
        _healthAmount = healthAmountMax;
    }

    public void Damage(int damageAmount)
    {
        _healthAmount -= damageAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);
        
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (IsDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool IsFullHealth()
    {
        return _healthAmount == healthAmountMax;
    }
    public bool IsDead()
    {
        return _healthAmount <= 0;
    }

    public int GetHealthAmount()
    {
        return _healthAmount;
    }    
    
    public int GetHealthAmountMax()
    {
        return healthAmountMax;
    }

    public float GetHealthAmountNormalized()
    {
        return (float) _healthAmount / healthAmountMax;
    }

    public void SetHealthAmountMax(int buildingTypeHealthAmountMax, bool updateHealthAmount)
    {
        healthAmountMax = buildingTypeHealthAmountMax;
        if (updateHealthAmount)
        {
            _healthAmount = healthAmountMax;
        }
    }
    public void Heal(int healAmount)
    {
        _healthAmount += healAmount;
        _healthAmount = Mathf.Clamp(_healthAmount, 0, healthAmountMax);
        
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealFull()
    {
        _healthAmount = healthAmountMax;
        OnHealed?.Invoke(this, EventArgs.Empty);
    }
}
