using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public DataContainer playerSO;

    private void Start()
    {
        playerSO.SetPlayerName("Hong");

        Debug.Log($"�÷��̾� �̸� : {playerSO.PlayerName}");
    }
}
