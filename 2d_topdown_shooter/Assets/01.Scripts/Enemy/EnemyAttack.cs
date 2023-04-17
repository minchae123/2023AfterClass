using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyBrain brain;

    public UnityEvent AttackFeedback;

    [SerializeField] protected float atkDelay = 1f;
    [SerializeField] protected int damage = 1;

    protected AIActionData actionData;

    protected virtual void Awake()
    {
        brain = GetComponent<EnemyBrain>();
        actionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public abstract void Attack();

    protected IEnumerator WaitBeforCoolTime()
    {
        actionData.isAttack = true;
        yield return new WaitForSeconds(atkDelay);
        actionData.isAttack = false;
    }
}
