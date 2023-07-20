using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public GameSoundSO settingsData;

    private void Start()
    {
        settingsData = Resources.Load<GameSoundSO>("GameSettingData");

        Debug.Log("���� ��� ����" + settingsData.soundEnabled);
        Debug.Log("���� ����" + settingsData.musicVolume);
        Debug.Log("ȿ���� ����" + settingsData.sfxVolume);
    }
}
