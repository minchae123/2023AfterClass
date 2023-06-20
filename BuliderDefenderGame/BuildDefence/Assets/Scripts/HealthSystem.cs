using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler OnDamaged;
    public event EventHandler OnDied;

    [SerializeField] private int healthAmountMax;
    private int healthAmount;

    private void Awake()
    {
        healthAmount = healthAmountMax;
    }

    public void Damage(int damageAmount)
    {

        healthAmount -= damageAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, healthAmountMax);

        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (isDead())
        {
            OnDied?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public bool isDead()
    { 
        return healthAmount == 0;
    }

    public bool isFull()
    {
        return healthAmount == healthAmountMax;
    }

    public int GetHealthAmount()
    {
        return healthAmount;
    }

    public float GetHealthAmountNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public void SetHealthAmountMax(int healthAmountMax)
    {
        this.healthAmountMax = healthAmountMax;
        healthAmount = healthAmountMax;
    }
}
