using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireBall : PoolableMono
{
    private Light2D light2D;
    public Light2D Light => light2D;
    public float lightMaxIntensity = 2.5f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    private bool isDead = false;

    [SerializeField] private LayerMask whatIsEnemy;
    [SerializeField] private BulletDataSO _bulletData;

    private void Awake()
    {
        light2D = transform.Find("Light").GetComponent<Light2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Flip(bool value)
    {
        spriteRenderer.flipX = value;
    }

    public void Fire(Vector2 dir)
    {
        rigid.velocity = dir * _bulletData.bulletSpeed;
    }

    public override void Init()
    {
        light2D.intensity = 0;
        transform.localScale = Vector3.one;
        rigid.velocity = Vector3.zero;
        isDead = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") || ((1 << collision.gameObject.layer) & whatIsEnemy) > 0) 
        {
            HitObstacle(collision);
            isDead = true;
            PoolManager.Instance.Push(this);
        }

    }

    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(_bulletData.impactObstaclePrefab.name) as ImpactScript;

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        Vector3 explosionPos = transform.position + transform.right * 0.5f;
        impact.SetPositionAndRotation(explosionPos, rot);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2.5f, whatIsEnemy);
        
        foreach(Collider2D collider in colliders)
        {
            if(collider.TryGetComponent(out IDamageable health))
            {
                Vector3 normal = (transform.position -  collider.transform.position).normalized;
                health.GetHit(_bulletData.damage, gameObject, collider.transform.position, normal);
            }
        }
    }
}
