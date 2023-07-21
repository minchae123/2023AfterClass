using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1920, 1080, true); // 해상도 설정
    }
}
