using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraCategory
{
    CannonCam = 0,
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

    private List<CameraSet> _camList;

    private CinemachineBasicMultiChannelPerlin bPerinNoise;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple Camera manager is running!");
        }
        Instance = this;

        _camList = new List<CameraSet>();

        //캐논 카메라 만들기. 
        var cannonCam = transform.Find("CannonCam").GetComponent<CinemachineVirtualCamera>();
        var rigCam = transform.Find("RigCam").GetComponent<CinemachineVirtualCamera>();
        var ballCam = transform.Find("BallCam").GetComponent<CinemachineVirtualCamera>();

        bPerinNoise = ballCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _camList.Add(new CameraSet { Category = CameraCategory.CannonCam, VCam = cannonCam });
        _camList.Add(new CameraSet { Category = CameraCategory.BallCam, VCam = ballCam });
        _camList.Add(new CameraSet { Category = CameraCategory.RigCam, VCam = rigCam });
    }

    public void SetFollowTarget(CameraCategory cat, Transform target)
    {
        foreach (CameraSet camSet in _camList)
        {
            if (camSet.Category == cat)
            {
                camSet.VCam.m_Follow = target;
                break;
            }
        }
    }

    public void SetActiveCam(CameraCategory cat, Transform followTarget = null)
    {
        foreach(CameraSet camSet in _camList)
        {
            if(camSet.Category == cat)
            {
                camSet.VCam.Priority = 15; //액티브인 애들은 높여주고
                
                if(followTarget != null)
                {
                    camSet.VCam.m_Follow = followTarget;
                }
            }
            else
            {
                camSet.VCam.Priority = 10; //액티브가 아닌 애들은 낮춰주고
            }
        }
    }

    public void StartShakee(float amplitude, float time)
    {

        StartCoroutine(StartCoroutine(amplitude, time));
    }

    IEnumerator StartCoroutine(float amplitude, float time) 
    {
        bPerinNoise.m_AmplitudeGain = amplitude;

        float p = 0;
        float currentTime = 0;
        while (p < 1)
        {
            p = currentTime / time;
            float a = Mathf.Lerp(amplitude, 0, p);
            bPerinNoise.m_AmplitudeGain = a;
            yield return null;
        }

        bPerinNoise.m_AmplitudeGain = 0;
    }
}
