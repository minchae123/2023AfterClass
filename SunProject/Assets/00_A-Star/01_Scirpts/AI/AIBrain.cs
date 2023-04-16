using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    public float ViewAngle;
    public float ViewRange;

    [SerializeField] private AIState curState; // ÇöÀç »óÅÂ

    public Transform PlayerTrm;
    
    public NavAgent NavAgentCompo { get; private set; }
    public AIStateInfo StateinfoCompo { get; private set; }

    private void Awake()
    {
        NavAgentCompo = GetComponent<NavAgent>();
        StateinfoCompo = transform.Find("AI").GetComponent<AIStateInfo>();
    }

    private void Update()
    {
        curState.UpdateState();
    }

    public void ChageState(AIState nextState)
    {
        curState = nextState;
    }

    public void Attack(bool isMelee)
    {
        NavAgentCompo.StopImmediately();
        StateinfoCompo.isAttack = true;
        if (isMelee)
        {
            StateinfoCompo.isMelee = true;
            StateinfoCompo.meleeCool = 1f;
            Debug.Log("¾Ó");
            StartCoroutine(DelayCo(0.3f, () => StateinfoCompo.isMelee = false));
        }
        else
        {
            StateinfoCompo.isRange = true;
            StateinfoCompo.rangeCool= 1f;
            StartCoroutine(DelayCo(0.8f, () => StateinfoCompo.isRange = false));
            Debug.Log("»§¾ß");
        }
    }

    IEnumerator DelayCo(float t, Action callBack)
    {
        yield return new WaitForSeconds(t);
        StateinfoCompo.isAttack = false;
        callBack?.Invoke();     
    }
}
