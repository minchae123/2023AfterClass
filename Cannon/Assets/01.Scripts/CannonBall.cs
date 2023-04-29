using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 dir, float power)
    {
        rigid.AddForce(dir * power, ForceMode2D.Impulse);
    }
}
