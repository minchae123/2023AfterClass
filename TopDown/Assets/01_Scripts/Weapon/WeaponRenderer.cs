using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRenderer : MonoBehaviour
{
    [SerializeField] protected int playerSortingOrder = 5;

    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void FlipSprite(bool value)
    {
        Vector3 localScale = new Vector3(1, 1, 1);
        if (value)
        {
            localScale.y = -1;
        }
        transform.localScale = localScale;
    }

    public void RenderBehindHead(bool value)
    {
        spriteRenderer.sortingOrder = value ? playerSortingOrder - 1 : playerSortingOrder + 1;
    }
}
