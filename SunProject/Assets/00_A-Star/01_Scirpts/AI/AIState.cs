using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AIAction> actions = null;
    public List<AITransition> transitions = null;

    protected AIBrain brain;

    private void Awake()
    {
        brain = transform.parent.parent.GetComponent<AIBrain>();

        actions = new List<AIAction>();
        GetComponents<AIAction>(actions);
        actions.ForEach(a => a.SetUp(brain));

        transitions = new List<AITransition>();
        GetComponentsInChildren<AITransition>(transitions);
        transitions.ForEach(t => t.Setup(brain));
    }

    public void UpdateState()
    {
        // ���� ���� �׼ǵ� ����
        foreach(AIAction a in actions)
        {
            a.TakeAction();
        }

        // ���� ������ ���鵵 ���� �Ǵ�
        foreach(AITransition t in transitions)
        {
            if (t.CheckTransition())
            {
                brain.ChageState(t.nextState);
                break;
            }
        }
    }
}
