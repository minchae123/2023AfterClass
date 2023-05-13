using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public event Action OnExplosion;

    private Rigidbody2D rigid;
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private float expRadius = 2.5f;

    [SerializeField] private Explosion expPrefab;

    [SerializeField] private float lifeTime = 3f;
    private float curLifeTime = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 dir, float power)
    {
        rigid.AddForce(dir * power, ForceMode2D.Impulse);
    }

    private void Update()
    {
        curLifeTime += Time.deltaTime;
        if(curLifeTime > lifeTime)
        {
            DestroyBall();
        }
    }

    private void DestroyBall()
    {
        OnExplosion?.Invoke();

        Explosion exp = Instantiate(expPrefab, transform.position, Quaternion.identity);
        exp.PlayParticle();

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] cols =  Physics2D.OverlapCircleAll(transform.position, expRadius, whatIsTarget);

        float maxPower = 5f;
        float minPower = 2f;

        foreach(Collider2D c in cols)
        {
            Box box = c.GetComponent<Box>();

            Vector2 dir = (c.transform.position - transform.position);

            float percent = dir.magnitude / expRadius;
            float power = Mathf.Lerp(maxPower, minPower, percent);

            box.DestroyBox(dir.normalized * power + new Vector2(0, 4f));
        }

        DestroyBall();
    }
}
