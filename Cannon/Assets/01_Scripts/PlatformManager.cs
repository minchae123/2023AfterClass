using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private Platform platformPrefab;

    [SerializeField] Vector2 randomYDelta;
    private List<Platform> platformList = new List<Platform>();

    private void Start()
    {
        float lastY = 0;
        for (int i = 0; i < 8; i++)
        {
            Platform p = Instantiate(platformPrefab, transform) as Platform;
            p.ResetPlatform(lastY);

            lastY = lastY + Random.Range(randomYDelta.x, randomYDelta.y);

        }
    }
}
