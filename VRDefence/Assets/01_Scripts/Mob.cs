using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Mob : MonoBehaviour
{
    private float hueMin = 0, hueMax = 1;
    private float saturationMin = 0.7f, saturationMax = 1;
    private float valueMin = 0.7f, valueMax = 1;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.SetDestination(new Vector3(-3f, 5f, 2f));
        agent.speed *= Random.Range(0.8f, 1.5f);
    }
}
