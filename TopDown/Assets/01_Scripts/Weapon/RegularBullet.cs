using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularBullet : PoolableMono
{
    public bool isEnemy; // 적의 총알인가 ?
    public int DamageFactor = 1; // 총알 데미지 계수

    [SerializeField] private float TTL;
    [SerializeField] private float timeToLive; // 몇초동안 살아남을 것인가 ?
    [SerializeField] private float bulletSpeed;

    private Rigidbody2D rigid;
    private bool isDead = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();    
    }

    private void FixedUpdate()
    {
        timeToLive += Time.fixedDeltaTime;
        rigid.MovePosition(transform.position + transform.right * bulletSpeed * Time.fixedDeltaTime);
        if(timeToLive >= TTL)
        {
            isDead = true;
            PoolManager.Instance.Push(this);
        }
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
