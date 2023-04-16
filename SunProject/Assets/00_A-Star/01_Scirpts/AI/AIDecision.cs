using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse;
    protected AIBrain brain;
    // 결정을 내리기 위해 필요한 다른 정보

    public virtual void Setup(AIBrain b)
    {
        brain = b;
    }

    public abstract bool MakeADecision()
;
}
