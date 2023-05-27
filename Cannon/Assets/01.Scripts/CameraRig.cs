using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField]
    private Transform _endTrm;

    [SerializeField]
    private float _moveSpeed = 8f;

    private float _startX, _endX;

    public void SetEndTrmPosition(Vector3 pos, Vector3 start)
    {
        _endTrm.position = pos;
        _startX = start.x; //현재 시작 x를 넣어주고
        _endX = _endTrm.position.x; //얘가 갈 수 있는 최대 거리의 x를 넣어주고
    }
    private void Awake()
    {
        _endTrm = transform.Find("EndTrm");
    }

    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        //1.이동할 양을 구한다. xMove* Time.deltaTime* moveSpeed
        //2.구한 양을 현재 좌표에 더한 벡터를 알아낸다.
        //3.해당 벡터의 x에 클램프를 적용한다.
        //4.클램프가 적용된 벡터를 현재 좌표에 넣는다.

        float xDelta = xMove * Time.deltaTime * _moveSpeed;
        Vector3 nextPos = transform.position + new Vector3(xDelta, 0, 0);
        nextPos.x = Mathf.Clamp(nextPos.x, _startX, _endX);
        transform.position = nextPos;
    }
}
