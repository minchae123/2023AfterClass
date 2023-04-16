using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    private SpriteRenderer spriteRenderer;
    private readonly int NormalHash = Shader.PropertyToID("_NormalState");

    public override void SetUp(AIBrain brain)
    {
        base.SetUp(brain);
        spriteRenderer = brain.GetComponent<SpriteRenderer>();
    }

    public override void TakeAction()
    {
        _brain.NavAgentCompo.StopImmediately();

        Material mat = spriteRenderer.material;
        mat.SetInt(NormalHash, 0);
    }
}
