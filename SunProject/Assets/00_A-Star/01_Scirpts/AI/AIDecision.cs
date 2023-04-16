using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse;
    protected AIBrain brain;
    // ������ ������ ���� �ʿ��� �ٸ� ����

    public virtual void Setup(AIBrain b)
    {
        brain = b;
    }

    public abstract bool MakeADecision()
;
}
