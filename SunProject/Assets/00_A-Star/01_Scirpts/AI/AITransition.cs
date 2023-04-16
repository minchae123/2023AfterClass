using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> decisions = null;
    public AIState nextState;


    public void Setup(AIBrain brain)
    {
        decisions = new List<AIDecision>();
        GetComponents<AIDecision>(decisions);
        decisions.ForEach(d => d.Setup(brain));
    }

    public bool CheckTransition()
    {
        bool result = false;
        foreach(AIDecision d in decisions)
        {
            result = d.MakeADecision();
            if (d.IsReverse)
                result = !result;
            if (result == false)
                break;
        }
        // ������� �Դµ� true�� ��� ���� ���� true
        return result;
    }
}
