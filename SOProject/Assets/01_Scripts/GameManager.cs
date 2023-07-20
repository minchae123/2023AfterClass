using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameSettingAsset")]
public class GameSetting : ScriptableObject
{
    public int soundVolume;
    public bool showTutorial;
}

public class GameManager : MonoBehaviour
{
    private string charaterPrefabPath = "Prefabs/Character";


    public GameSetting gameSetting;

    private void Start()
    {
        gameSetting = Resources.Load<GameSetting>("GameSettingAsset");

        Debug.Log("사운드 볼륨 : " + gameSetting.soundVolume);
        Debug.Log("튜토리얼 표시 여부 : " + gameSetting.showTutorial);
    }
}
