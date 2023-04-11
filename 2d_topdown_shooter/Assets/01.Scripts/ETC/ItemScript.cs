using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : PoolableMono
{
    [SerializeField] private ResourceDataSO itemData;
    public ResourceDataSO ItemData => itemData;

    private AudioSource audioSource;
    private Collider2D col;
    private SpriteRenderer spriterenderer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = itemData.useSound;
        col = GetComponent<Collider2D>();
        spriterenderer = GetComponent<SpriteRenderer>();
    }

    public void PickUpResource()
    {
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        col.enabled = false;
        spriterenderer.enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length + 0.3f);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Item");
        col.enabled = true;
        spriterenderer.enabled = true;
    }
}
