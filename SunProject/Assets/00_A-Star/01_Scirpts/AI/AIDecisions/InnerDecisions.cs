using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDecisions : AIDecision
{
    [SerializeField]
    [Range(0.1f, 30f)]
    private float distance = 5f;
    public override bool MakeADecision()
    {
        float calc = Vector2.Distance(brain.PlayerTrm.position, transform.position);

        if (calc < distance)
        {
            // 적을 발견하면 라스트 포지션 갱신
            brain.StateinfoCompo.lastEnemyPosition = brain.PlayerTrm.position;
            return true;
        }
        else
            return false;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distance);
            Gizmos.color = Color.white;
        }
    }
#endif
}
