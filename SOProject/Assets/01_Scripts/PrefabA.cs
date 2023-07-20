using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabA : MonoBehaviour
{
    public ShareDB shareDB;

    private void Start()
    {
        shareDB = Resources.Load<ShareDB>("ShareDB");

        Debug.Log("Prefab A���� ������ �� " + shareDB.SharedValue);
    }
}
