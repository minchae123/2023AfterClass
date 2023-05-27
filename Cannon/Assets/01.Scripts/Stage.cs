using UnityEngine;
using UnityEngine.Tilemaps;

public class Stage : MonoBehaviour
{
    private Transform _boxParent;
    private Transform _endPointTrm;
    private Transform _playerPointTrm;
    
    public int BallCount;
    public Vector3 EndPoint => _endPointTrm.position;
    public Vector3 PlayerPoint => _playerPointTrm.position;
    
    public int BoxCount => _boxParent.childCount; //현재 박스 갯수
    private int _totalBoxCount = 0;
    public int TotalBoxCount => _totalBoxCount;

    private Tilemap groundTilemap;
    public Tilemap GroundTilemap => groundTilemap;

    private void Awake()
    {
        _boxParent = transform.Find("Boxes");
        _totalBoxCount = _boxParent.childCount;

        _endPointTrm = transform.Find("EndTrm");
        _playerPointTrm = transform.Find("PlayerPositionTrm");

        groundTilemap = transform.Find("LevelTilemap/Ground").GetComponent<Tilemap>();
    }
}
