using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManager 
{
    private Tilemap floorMap;
    private Tilemap collisionMap;

    public static TileMapManager Instance;

    public TileMapManager(Transform tileMapParent)
    {
        floorMap = tileMapParent.Find("Floor").GetComponent<Tilemap>();
        collisionMap = tileMapParent.Find("Collision").GetComponent<Tilemap>();

        floorMap.CompressBounds();
    }

    public bool CanMove(Vector3Int pos)
    {
        BoundsInt mapBound = floorMap.cellBounds;
        // �� �ٱ��� ������ ��ǥ ��û
        if(pos.x < mapBound.xMin || pos.x > mapBound.xMax || pos.y < mapBound.yMin || pos.y > mapBound.yMax)
        {
            return false;
        }
        return collisionMap.GetTile(pos) == null;
    }

    public Vector3Int GetTilePos(Vector3 worldPos)
    {
        return floorMap.WorldToCell(worldPos); // ���� ��ǥ ������ Ÿ�ϸ� �� ��ǥ
    }

    public Vector3 GetWolrdPos(Vector3Int cellPos)
    {
        return floorMap.GetCellCenterWorld(cellPos); // ���� �߽��� ����
    }
}
