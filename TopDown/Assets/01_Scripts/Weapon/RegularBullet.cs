using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : PoolableMono
{
    public bool isEnemy; // 적의 총알인가 ?
    [SerializeField] BulletDataSO bulletData;
    [SerializeField] private float timeToLive; // 몇초동안 살아남을 것인가 ?

    private Rigidbody2D rigid;
    private bool isDead = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        timeToLive += Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + transform.right * bulletData.bulletSpeed * Time.fixedDeltaTime);
        if (timeToLive >= bulletData.lifeTime)
        {
            isDead = true;
            PoolManager.Instance.Push(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        HitObstacle(collision);

    }

    private void HitObstacle(Collider2D collision)
    {
        ImpactScript impact = PoolManager.Instance.Pop(bulletData.impactEnemyPrefab.name) as ImpactScript;
        
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360f)));
        impact.SetPositionAndRotation(collision.transform.position, rot);
    }

    private void HitEnemy(Collider2D collision)
    {

    }

    public void SetPositionAndRoatation(Vector3 pos, Quaternion rot)
    {
        transform.SetPositionAndRotation(pos, rot);
    }

    public override void Reset()
    {
        isDead = false;
        timeToLive = 0;
    }
}
