using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAttack
{
    public override void Attack()
    {
        if(actionData.isAttack == false)
        {
            if(brain.Target.TryGetComponent<IDamageable>(out IDamageable health))
            {
                actionData.isAttack = true;
                AttackFeedback?.Invoke() ;
                Vector3 normal = (brain.transform.position - brain.Target.position).normalized;
                health.GetHit(damage, brain.gameObject, brain.Target.position, normal);
                StartCoroutine(WaitBeforCoolTime());
            }
        }
    }
}
