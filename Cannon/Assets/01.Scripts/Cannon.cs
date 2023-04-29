using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private float rotateSpeed;
    private float curRotate;

    private Transform barrelTrm;

    private void Awake()
    {
        barrelTrm = transform.Find("CannonBarrel").GetComponent<Transform>();
    }

    private void Update()
    {
        float y = Input.GetAxisRaw("Vertical");
        curRotate += y * rotateSpeed * Time.deltaTime;
        curRotate = Mathf.Clamp(curRotate, 0, 90);

        barrelTrm.rotation =  Quaternion.Euler(0, 0, curRotate);
    }
}
