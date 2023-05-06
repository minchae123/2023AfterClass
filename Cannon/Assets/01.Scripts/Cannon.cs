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

        UIManager.Instance.SetAngleText(curRotate);
    }

    private void CheckFire()
    {
        if (Input.GetButtonDown("Jump"))
        {
            // ¹æÇâ Èû ¹«¾ù
            curPower = 0;
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
        }

        UIManager.Instance.FillGaugeBar(curPower, maxPower);
    }
}
