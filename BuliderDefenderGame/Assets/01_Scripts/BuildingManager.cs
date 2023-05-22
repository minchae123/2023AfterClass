using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private BuildingTypeSO pfWoodHarvester;
    [SerializeField] private BuildingTypeSO pfStoneHarvester;
    [SerializeField] private BuildingTypeSO pfGoldHarvester;

    private Transform selectBuilding;

    private void Awake()
    {
        selectBuilding = pfWoodHarvester.prefab;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(selectBuilding, GetMousePos(), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            selectBuilding = pfWoodHarvester.prefab;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            selectBuilding = pfStoneHarvester.prefab;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            selectBuilding = pfGoldHarvester.prefab;
        }

    }

    private Vector3 GetMousePos()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;
        return mouseWorldPos;
    }
}
