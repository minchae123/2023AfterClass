using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerData playerData;
    
    private void Start()
    {
        playerData = Resources.Load<PlayerData>("PlayerData");

        Debug.Log($"�÷��̾� �̸� {playerData.PlayerName}");

        playerData.PlayerName = "��ì";
    }
}
