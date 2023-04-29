using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum BallState
{
    IDLE = 0,
    CHARGING = 1,
    FIRE = 2,
}

public class BallController : MonoBehaviour
{
    public Transform BallDirectionTrm;
    public Transform PowerGaugeTrm;
    [SerializeField] private float maxPower = 15f, chargeSpeed = 20f;
    [SerializeField] private BallState state = BallState.IDLE; 
    private float curPower = 0;
    private Rigidbody2D rigid;
    private Vector2 inputDir;

    private Camera mainCam;
    private SpriteRenderer arrowRenderer;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        arrowRenderer = transform.Find("BallDirection").GetComponent<SpriteRenderer>();

        mainCam = Camera.main;
    }

    void Update()
    { 

        if(state == BallState.IDLE)
        {
            inputDir = ((Vector2)mainCam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position).normalized;
            float angle = Mathf.Atan2(inputDir.y, inputDir.x);

            BallDirectionTrm.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
        

        if (Input.GetMouseButtonDown(0) && state == BallState.IDLE)
        {
            state = BallState.CHARGING;
            TimeController.Instance.SetTimeScale(0.4f);
            CameraManager.Instance.Zoom(4f);
        }

        if (Input.GetMouseButton(0) && state == BallState.CHARGING)
        {
            curPower += chargeSpeed * Time.unscaledDeltaTime;
            curPower = Mathf.Clamp(curPower, 0, maxPower);
            PowerGaugeTrm.localScale = new Vector3(curPower / maxPower, 1, 1);
        }

        if (Input.GetMouseButtonUp(0) && state == BallState.CHARGING)
        {
            TimeController.Instance.SetTimeScale(1);
            CameraManager.Instance.Zoom(7f);

            state = BallState.IDLE;
            //state = BallState.FIRE;
            Fire();
        }
    }

    private void Fire()
    {
        rigid.velocity = Vector2.zero; // Á¤ÁöÇÏ°í Èû
        rigid.AddForce(inputDir.normalized * curPower, ForceMode2D.Impulse);
        curPower = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        state = BallState.IDLE;
    }
}
