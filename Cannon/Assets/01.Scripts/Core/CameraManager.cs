using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraCategory
{
    CannonCam =0,
    BallCam = 1,
    RigCam = 2
}

public struct CameraSet
{
    public CameraCategory Category;
    public CinemachineVirtualCamera VCam;
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private List<CameraSet> camList;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("카메라 오류");
        }
        Instance = this;

        camList = new List<CameraSet>();

        var cannonCam = transform.Find("CannonCam").GetComponent<CinemachineVirtualCamera>();
        var rigCam = transform.Find("RigCam").GetComponent<CinemachineVirtualCamera>();
        var ballCam = transform.Find("BallCam").GetComponent<CinemachineVirtualCamera>();

        camList.Add(new CameraSet { Category = CameraCategory.CannonCam, VCam = cannonCam });
        camList.Add(new CameraSet { Category = CameraCategory.RigCam, VCam = rigCam });
        camList.Add(new CameraSet { Category = CameraCategory.BallCam, VCam = ballCam });
    }

    public void SetActiveCam(CameraCategory cat, Transform followTarget = null)
    {
        foreach(CameraSet camSet in camList)
        {
            if(camSet.Category == cat)
            {
                camSet.VCam.Priority = 15;

                if(followTarget != null)
                {
                    camSet.VCam.m_Follow = followTarget;
                }
            }
            else
            {
                camSet.VCam.Priority = 10;
            }
        }
    }

    public void SetFollowTarget(CameraCategory cat, Transform target)
    {
        foreach (CameraSet camSet in camList)
        {
            if (camSet.Category == cat)
            {
                camSet.VCam.m_Follow = target;
                break;
            }
        }
    }
}
