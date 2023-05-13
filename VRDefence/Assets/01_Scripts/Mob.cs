using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class Mob : MonoBehaviour
{
    public UnityEvent OnCreated;
    public UnityEvent OnDestroy;


    public float destroyDelay = 1f;
    private bool isDestroy = false;

    private void Awake()
    {
    }

    private void Start()
    {
        OnCreated?.Invoke();

        Invoke(nameof(Destroy), 3f);
    }

    private void Destroy()
    {
        if (isDestroy)
            return;
        isDestroy = true;

        OnDestroy?.Invoke();
        Destroy(gameObject, destroyDelay);
    }
}
