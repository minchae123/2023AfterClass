using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;

    private float timer;
    private float timerMax;
    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, resourceGeneratorData.resourceDetectionRadius);

        int nearbyResoureAmount = 0;
        foreach (Collider2D collider2D in collider2DArray)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();
            if (resourceNode != null)
            {
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    nearbyResoureAmount++;
                }
            }
        }

        nearbyResoureAmount = Mathf.Clamp(nearbyResoureAmount, 0, resourceGeneratorData.maxResourceAmount);

        if (nearbyResoureAmount == 0)
        {
            enabled = false;
            transform.Find("pfResourceGeneratorOverlay").gameObject.SetActive(false);
        }
        else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) +
                resourceGeneratorData.timerMax *
                (1 - (float)nearbyResoureAmount / resourceGeneratorData.maxResourceAmount);
        }

    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetAmountGeneratePerSecond()
    {
        return 1 / timerMax;
    }

    public float GetTimerNomarlized()
    {
        return timer / timerMax;
    }
}
