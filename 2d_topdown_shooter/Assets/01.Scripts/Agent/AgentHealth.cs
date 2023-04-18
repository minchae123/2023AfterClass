using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHP;
    private int curHp;

    public UnityEvent<int> OnInitHealth = null;
    public UnityEvent<int, int> OnHealthChanged = null;

    public int Health
    {
        get => curHp;
        set
        {
            curHp = value;
            curHp = Mathf.Clamp(curHp, 0, maxHP);
        }
    }

    [SerializeField] private bool isDead = false;

    public bool IsEnemy => false;
    public Vector3 hitPoint { get; set; }

    public UnityEvent OnGetHit = null;
    public UnityEvent OnDead = null;

    private void Start()
    {
        curHp = maxHP;
        OnInitHealth?.Invoke(maxHP);
        OnHealthChanged?.Invoke(curHp, maxHP);
    }

    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (isDead) return;
        Health -= damage;
        if (Health <= 0)
        {
            OnDead?.Invoke();
            isDead = true;
        }
        OnHealthChanged?.Invoke(curHp, maxHP);
    }

    public void AddHealth(int value)
    {
        Health += value;
        OnHealthChanged?.Invoke(curHp, maxHP);
    }

}