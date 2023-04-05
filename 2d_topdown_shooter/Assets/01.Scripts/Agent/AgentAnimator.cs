using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentAnimator : MonoBehaviour
{
    protected Animator animator;
    protected readonly int walkBoolHash = Animator.StringToHash("Walk");

    public UnityEvent OnFootStep = null;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void AnimatePlayer(float velocity)
    {
        SetWalkAnimation(velocity > 0);
    }

    public void SetWalkAnimation(bool value)
    {
        animator.SetBool(walkBoolHash, value);
    }

    public void FootStepEvent()
    {
        OnFootStep?.Invoke();
    }
}
