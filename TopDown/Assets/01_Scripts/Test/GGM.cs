using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class GGM : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    private Vector2 destination;
    private Camera mainCam;
    //private Tween t = null;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 0);
    }

    void Start()
    {
        mainCam = Camera.main;
        destination = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            destination = mainCam.ScreenToWorldPoint(Input.mousePosition);

            Sequence seq = DOTween.Sequence();
            seq.Append(sr.DOFade(1, 1));
            seq.Append(transform.DOMove(destination, 1));
            seq.Join(transform.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetLoops(4));
            seq.AppendInterval(1);
            seq.AppendCallback(() => Debug.Log("--"));
            // 시퀀스는 스타트가 없고, 등록하면 다음프레임에서 바로 실행
        }
        //MoveToDestination();
    }

    private void MoveToDestination()
    {
        Vector3 dir = (Vector3)destination - transform.position;
        if (dir.magnitude > 0.1f)
        {
            transform.Translate(dir.normalized * Time.deltaTime * speed);
        }
    }
}
