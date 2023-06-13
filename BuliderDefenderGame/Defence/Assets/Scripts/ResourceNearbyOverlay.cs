using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNearbyOverlay : MonoBehaviour
{
    public void Show(ResourceGeneratorData data)
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
