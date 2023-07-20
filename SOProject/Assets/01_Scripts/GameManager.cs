using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSetting resoucePath;

    private void Start()
    {
        resoucePath = Resources.Load<GameSetting>("ResoucePathAsset");

        GameObject characterPrefab = Resources.Load<GameObject>(resoucePath.characterPrefabPath);

        Instantiate(characterPrefab);
    }
}
