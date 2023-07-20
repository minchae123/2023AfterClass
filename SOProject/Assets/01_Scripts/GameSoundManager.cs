using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSoundManager : MonoBehaviour
{
    public GameSoundSO settingsData;

    private void Start()
    {
        settingsData = Resources.Load<GameSoundSO>("GameSettingData");

        Debug.Log("사운드 사용 여부" + settingsData.soundEnabled);
        Debug.Log("음악 볼륨" + settingsData.musicVolume);
        Debug.Log("효과음 볼륨" + settingsData.sfxVolume);
    }
}
