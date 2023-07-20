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

        Debug.Log("���� ���� : " + gameSetting.soundVolume);
        Debug.Log("Ʃ�丮�� ǥ�� ���� : " + gameSetting.showTutorial);
    }
}
