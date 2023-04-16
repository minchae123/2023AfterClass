using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    private Vector3Int beforePos = Vector3Int.zero;

    private SpriteRenderer spriteRenderer;
    private readonly int NormalHash = Shader.PropertyToID("_NormalState");

    public override void SetUp(AIBrain brain)
    {
        base.SetUp(brain);
        spriteRenderer = brain.GetComponent<SpriteRenderer>();
    }

    public override void TakeAction()
    {
        Vector3Int nextPos = TileMapManager.Instance.GetTilePos(_brain.StateinfoCompo.lastEnemyPosition);

        if(nextPos != beforePos)
        {
            // 플레이어가 타일단위로 이동을 했다고 판단하고 움직임 시작
            _brain.NavAgentCompo.Destiation = nextPos;
            beforePos = nextPos;
        }

        Material mat = spriteRenderer.material;
        mat.SetInt(NormalHash, 0);
    }
}
