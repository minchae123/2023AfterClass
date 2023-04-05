using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        Vector2 dir = enemyBrain.Target.position - enemyBrain.BasePosition.position;
        enemyBrain.Move(dir.normalized, enemyBrain.Target.position);
    }
}
