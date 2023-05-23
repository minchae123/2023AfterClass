using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private BuildingTypeListSO buildingTypeList;
    private BuildingTypeSO buildingType;
    private Camera mainCamera;

    private void Awake()
    {
        buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
    }
    private void Start()
    {
        buildingType = buildingTypeList.list[0];
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            buildingType = buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            buildingType = buildingTypeList.list[1];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            buildingType = buildingTypeList.list[2];
        }
        if(Input.GetKeyDown(KeyCode.R)){
            buildingType = buildingTypeList.list[3];
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
