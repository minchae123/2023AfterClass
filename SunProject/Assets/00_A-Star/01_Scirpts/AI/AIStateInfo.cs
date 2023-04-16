using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateInfo : MonoBehaviour
{
    public Vector3 lastEnemyPosition;
    public float meleeCool = 0f;
    public float rangeCool = 0f;

    public bool isAttack;
    public bool isRange;
    public bool isMelee;

    public bool isArrived; // 목적지 도착 여부

    private void Update()
    {
        if(meleeCool > 0)
        {
            meleeCool -= Time.deltaTime;
            if (meleeCool < 0)
                meleeCool = 0;
        }
        if(rangeCool > 0)
        {
            rangeCool -= Time.deltaTime;
            if (rangeCool < 0)
                rangeCool = 0;
        }
    }
}
