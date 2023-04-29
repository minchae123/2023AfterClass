using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private float curRotate;
    private Transform barrelTrm;

    [SerializeField] private CannonBall ballPrefab;
    [SerializeField] private float chargeSpeed = 80, maxPower = 120f;
    private float curPower = 0;
    private Transform firePos;

    private void Awake()
    {
        barrelTrm = transform.Find("CannonBarrel").GetComponent<Transform>();
        firePos = transform.Find("CannonBarrel/FirePos").GetComponent<Transform>();
    }

    private void Update()
    {
        CheckRotate();
        CheckFire();
    }

    private void CheckRotate()
    {
        float y = Input.GetAxisRaw("Vertical");
        curRotate += y * rotateSpeed * Time.deltaTime;
        curRotate = Mathf.Clamp(curRotate, 0, 90);

        barrelTrm.rotation = Quaternion.Euler(0, 0, curRotate);
    }

    private void CheckFire()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // 방향 힘 무엇
        
        }

        if (Input.GetButton("Jump"))
        {
            curPower += chargeSpeed * Time.deltaTime;
            curPower = Mathf.Clamp(curPower, 0, maxPower);
        }

        if (Input.GetButtonUp("Jump"))
        {
            CannonBall ball = Instantiate(ballPrefab, firePos.position, Quaternion.identity);
            ball.Fire(firePos.right, curPower);
            curPower = 0;
        }

        // 점프 누르면 차징 시작되고
        // 점프 떼는 순간 차징 된 값으로 발사
        // 차징된 파워는 맥스 값 넘어갈 수 없음

    }
}
