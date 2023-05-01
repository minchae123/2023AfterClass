using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public bool IsEnemy { get; set; }
    public Vector3 hitPoint { get; private set; }

    protected bool isDead = false; //��� ���θ� ��Ÿ���� ��

    [SerializeField]
    protected int maxHealth; //�̰� ���߿� SO�� ���Ե� �Ŵ�.

    protected int curHealth;

    public UnityEvent OnGetHit = null;  //�¾��� �� �߻��� �̺�Ʈ��
    public UnityEvent OnDie = null;  // �׾��� �� �߻��� �̺�Ʈ

    private AIActionData _aiActionData;

    private HealthBarUI healthUI;

    private void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
        curHealth = maxHealth;
        healthUI = transform.Find("HealthBar").GetComponent<HealthBarUI>();
    }

    public void GetHit(int damage, GameObject damageDealer, Vector3 hitPoint, Vector3 normal)
    {
        if (isDead) return;

        curHealth -= damage;

        _aiActionData.hitPoint = hitPoint;
        _aiActionData.hitNormal = normal;

        OnGetHit?.Invoke();

        if(healthUI.gameObject.activeSelf == false)
            healthUI.gameObject.SetActive(true);

        healthUI.SetHealth(curHealth);

        if(curHealth <= 0)
        {
            DeadProcess();
        }
    }

    private void DeadProcess()
    {
        isDead = true;
        OnDie?.Invoke();
    }

    public void Reset()
    {
        curHealth = maxHealth;
        isDead = false;
        healthUI.SetHealth(curHealth);
        healthUI.gameObject.SetActive(false);
    }
}
