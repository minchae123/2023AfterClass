using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy { get; set; }
    public Vector3 hitPoint { get; private set; }

    protected bool idDead = false; //��� ���θ� ��Ÿ���� ��

    [SerializeField]
    protected int maxHealth; //�̰� ���߿� SO�� ���Ե� �Ŵ�.

    protected int curHealth;

    public UnityEvent OnGetHit = null;  //�¾��� �� �߻��� �̺�Ʈ��
    public UnityEvent OnDie = null;  // �׾��� �� �߻��� �̺�Ʈ

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
