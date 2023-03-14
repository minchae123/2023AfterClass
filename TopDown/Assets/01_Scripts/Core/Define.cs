using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    private static Camera mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if (mainCam == null) mainCam = Camera.main;
            return mainCam;
        }
    }
}
