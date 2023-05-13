using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonState : short
{
    IDLE = 0,
    MOVING = 1,
    CHARGING = 2,
    FIRE = 3,
    WAITING = 4,
}

public class Cannon : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private float curRotate;
    
    private Transform barrelTrm;
    private Transform camRigTrm;

    [SerializeField] private CannonBall ballPrefab;
    [SerializeField] private float chargeSpeed = 80, maxPower = 120f;
    private float curPower = 0;

    private Transform firePos;

    [SerializeField] private CannonState curState;

    private void Awake()
    {
        curState = CannonState.IDLE;
        barrelTrm = transform.Find("CannonBarrel").GetComponent<Transform>();
        firePos = transform.Find("CannonBarrel/FirePos").GetComponent<Transform>();
        camRigTrm = transform.Find("CameraRig");
    }

    private void Update()
    {
        if((short)curState < 2)
        {
            CheckRotate();
        }

        CheckFire();

        CheckWait();
    }

    private void CheckWait()
    {
        if (curState != CannonState.WAITING) return;

        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(ChangeToIdle());
        }
    }

    private IEnumerator ChangeToIdle()
    {
        UIManager.Instance.HideText();
        CameraManager.Instance.SetActiveCam(CameraCategory.RigCam);
        yield return new WaitForSeconds(1);
        curState = CannonState.IDLE;

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
        if (Input.GetButtonDown("Jump") && (short)curState < 2) 
        {
            curState = CannonState.CHARGING;
            curPower = 0;
        }

        if (Input.GetButton("Jump") && curState == CannonState.CHARGING)
        {
            curPower += chargeSpeed * Time.deltaTime;
            curPower = Mathf.Clamp(curPower, 0, maxPower);
        }

        if (Input.GetButtonUp("Jump") && curState == CannonState.CHARGING)
        {
            curState = CannonState.FIRE;

            StartCoroutine(FireSequence());
        }

        UIManager.Instance.FillGaugeBar(curPower, maxPower);
    }
    
    private void HandleAfterExplosion()
    {
        //CameraManager.Instance.SetActiveCam(CameraCategory.RigCam);
        //curState = CannonState.IDLE;
        curState = CannonState.WAITING;
        UIManager.Instance.ShowText("Press Space Key To Continue");
    }

    private IEnumerator FireSequence()
    {
        CameraManager.Instance.SetActiveCam(CameraCategory.CannonCam);
        CameraManager.Instance.SetFollowTarget(CameraCategory.BallCam, barrelTrm);

        yield return new WaitForSeconds(1.5f);

        camRigTrm.localPosition = Vector3.zero;

        CannonBall ball = Instantiate(ballPrefab, firePos.position, Quaternion.identity);
        CameraManager.Instance.SetActiveCam(CameraCategory.BallCam, ball.transform);

        ball.Fire(firePos.right, curPower);
        ball.OnExplosion += HandleAfterExplosion;
    }
}
