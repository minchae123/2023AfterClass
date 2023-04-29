using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAttackDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return !actionData.isAttack; // 공격중일때 false 아니면 true
    }
}
