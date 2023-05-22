using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Transform pfWoodHarvester;
    [SerializeField] private Transform pfStoneHarvester;
    [SerializeField] private Transform pfGoldHarvester;

    private Transform selectBuilding;

    private void Awake()
    {
        selectBuilding = pfWoodHarvester;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(selectBuilding, GetMousePos(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectBuilding = pfWoodHarvester;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            selectBuilding = pfStoneHarvester;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            selectBuilding = pfGoldHarvester;
        }

    }

    private Vector3 GetMousePos()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
