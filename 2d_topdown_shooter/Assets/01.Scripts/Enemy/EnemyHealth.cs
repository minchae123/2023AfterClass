using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy { get; set; }
    public Vector3 hitPoint { get; private set; }

    protected bool idDead = false; //사망 여부를 나타내는 것

    [SerializeField]
    protected int maxHealth; //이건 나중에 SO로 빼게될 거다.

    protected int curHealth;

    public UnityEvent OnGetHit = null;  //맞았을 때 발생할 이벤트랑
    public UnityEvent OnDie = null;  // 죽었을 때 발생할 이벤트

    private AIActionData _aiActionData;

    private void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
        curHealth = maxHealth; 
    }

    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (idDead) return;

        Debug.Log(damage);
        curHealth -= damage;
        
        _aiActionData.hitPoint = hitPoint;
        _aiActionData.hitNormal = normal;

        OnGetHit?.Invoke();
        

        if(curHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        idDead = true;
        OnDie?.Invoke();
    }
}
