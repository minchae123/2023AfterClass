using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private Transform playerTrm;
    public Transform PlayerTrm => playerTrm;

    [SerializeField] private Transform tileMapParent;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Multiple Gamemanager is running");
        }
        Instance = this;

        TileMapManager.Instance = new TileMapManager(tileMapParent);
    }
}
