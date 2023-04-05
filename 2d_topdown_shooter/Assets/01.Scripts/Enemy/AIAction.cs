using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIActionData actionData;
    protected EnemyBrain enemyBrain;

    public virtual void SetUp(Transform parentTrm)
    {
        parentTrm.Find("AI").GetComponent<AIActionData>();
        enemyBrain = parentTrm.GetComponent<EnemyBrain>();
    }

    public abstract void TakeAction(); //수행할 작업 여기다가
}
