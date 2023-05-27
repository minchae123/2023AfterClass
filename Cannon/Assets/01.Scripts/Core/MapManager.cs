using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager 
{
    public static MapManager Instance;

    private Tilemap groundTilemap;

    public void SetTilemap(Tilemap map)
    {
        groundTilemap = map;
    }

    public void SetExplosion(Vector3 worldCenterPos, float radius)
    {
        if (groundTilemap == null) return;

        Vector3Int tilePos = groundTilemap.WorldToCell(worldCenterPos);

        int r = Mathf.CeilToInt(radius); // 내림 함수 int

        for(int i = -r; i <= r; i++)
        {
            for (int j = -r; j <= r; j++) 
            {
                int d = Mathf.Abs(i) + Mathf.Abs(j);
                if (d > r) continue;

                Vector3Int offset = new Vector3Int(j, i);
                Vector3Int target = tilePos + offset;

                var tile = groundTilemap.GetTile(target);

                if(tile != null)
                {
                    groundTilemap.SetTile(target, null);
                }
            }
        }
    }
}
