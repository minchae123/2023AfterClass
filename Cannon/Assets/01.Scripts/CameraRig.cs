using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    [SerializeField] private Transform endTrm;
    [SerializeField] float moveSpeed = 8f;

    private float startX, endX;

    private void Awake()
    {
        endTrm = transform.Find("EndTrm");

        startX = transform.position.x;
        endX = endTrm.position.x;
    }

    private void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal");

        float xDelta = xMove * Time.deltaTime * moveSpeed;
        Vector3 nextPos = new Vector3(xDelta, 0, 0) + transform.position;
        nextPos.x = Mathf.Clamp(nextPos.x, startX, endX);
        transform.position = nextPos;
    }
}
