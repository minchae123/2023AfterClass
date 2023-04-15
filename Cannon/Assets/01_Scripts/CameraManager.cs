using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] CinemachineVirtualCamera followCam;
    private Camera mainCam;
    public Camera MainCam
    {
        get { if(mainCam == null) mainCam = Camera.main; return mainCam; }
    }

    public float CamWidth
    {
        get { float width = MainCam.aspect * MainCam.orthographicSize; return width; }
    }

    public bool CheckIsInCamY(float y) // 넣은 y값이 카메라 안쪽에 있는가
    {
        return y > transform.position.y - MainCam.orthographicSize && y < transform.position.y + MainCam.orthographicSize;
 }

    public Vector2 CamWidthBound() // 카메라 너비의 외곽을 반환
    {
        float x = transform.position.x;
        Vector2 bound = new Vector2(x - CamWidth, x + CamWidth);
        return bound;
    }


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("오류");
        }
        Instance = this;
    }

    private void Start()
    {
        Debug.Log(CamWidth);
    }

    public void Zoom(float value, float time = 0.3f)
    {
        DOTween.To(
            () => followCam.m_Lens.OrthographicSize,
            x => followCam.m_Lens.OrthographicSize = x, 
            value, 
            time);
        followCam.m_Lens.OrthographicSize = value;
    }
}
