using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody2D rigid;
    [SerializeField] private LayerMask whatIsTarget;
    [SerializeField] private float expRadius = 2.5f;

    [SerializeField] private Explosion expPrefab;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 dir, float power)
    {
        rigid.AddForce(dir * power, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] cols =  Physics2D.OverlapCircleAll(transform.position, expRadius, whatIsTarget);

        float maxPower = 5f;
        float minPower = 2f;

        foreach(Collider2D c in cols)
        {
            Box box = c.GetComponent<Box>();

            Vector2 dir = (c.transform.position - transform.position).normalized;

            // 거리에 반비례해서 거리가 가까우면 5 멀어지면 2f 점진적으로 낮아지도록 하되 2f 밑으로는 안떨어짐
            // explosionRadius , lerp 잘 활용 
            
            box.DestroyBox(dir);
        }

        Explosion exp = Instantiate(expPrefab, transform.position, Quaternion.identity);
        exp.PlayParticle();

        Destroy(gameObject);
    }
}
