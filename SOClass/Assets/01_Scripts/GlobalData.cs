using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GlobalDB")]
public class GlobalData : ScriptableObject
{

    private static GlobalData instance;
    public static GlobalData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GlobalData();
            }
            return instance;
        }
    }

    public int playerExperience = 0;
}
