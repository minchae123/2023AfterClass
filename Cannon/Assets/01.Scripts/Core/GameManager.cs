using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private Cannon _playerCannon;  //플레이어 캐논 스크립트
    public Transform PlayerTrm => _playerCannon.transform;
    
    [SerializeField] private StageDataSO _dataList;
    private Dictionary<int, Stage> _stageDictionary;

    private Stage _currentStage = null;

    private int _maxBallCount = 0;
    private int _currentBallCount = 0;
    private int _currentBoxCount = 0;

    public event Action OnLoadStageComplete;
    public event Action OnLoadStageStart; //이건 나중에 쓰게 된다.
    public event Action<LabelCategory, string> OnChangeLabel;
    public event Action<int, int> OnGameOver; // 남은 박스 남은 캐논


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple Game manager is running!");
        }
        Instance = this;

        CreateUIManager();
        CreateStageDictionary();
        CreateTimeController();
        CreateMapManager();
        _playerCannon.Init(); //플레이어 캐논 초기화 및 이벤트 구독
    }

    private void CreateMapManager()
    {
        MapManager.Instance = new MapManager();
    }

    private void CreateTimeController()
    {
        TimeController.Instance = gameObject.AddComponent<TimeController>();
    }

    private void CreateUIManager()
    {
        UIManager.Instance = GameObject.Find("Canvas").AddComponent<UIManager>();
        UIManager.Instance.Init(this);
    }

    private void CreateStageDictionary()
    {
        _stageDictionary = new Dictionary<int, Stage>();
        _dataList.StagePair.ForEach( p => _stageDictionary.Add(p.Level, p.Prefab));

        // foreach (StageData s in _dataList.StagePair)
        // {
        //     _stageDictionary.Add(s.Level, s.Prefab);
        // }
    }
    
    private void Start()
    {
        LoadStage(1);
    }

    public void LoadStage(int level)
    {
        if (_currentStage != null)
        {
            Destroy(_currentStage.gameObject);
        }

        // out : 함수에 빈 껍데기만 주면 함수가 해당껍데기를 채워주는거고 , ref : 함수에 값이 있는 객체를 넘겨줘
        if (_stageDictionary.TryGetValue(level, out var prefab))
        {
            _currentStage = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            _currentBallCount = _maxBallCount = _currentStage.BallCount;
            _currentBoxCount = _currentStage.TotalBoxCount;
            
            //엔드위치와 플레이어 위치가 셋팅 완료
            _playerCannon.CameraRigCompo.SetEndTrmPosition( _currentStage.EndPoint, _currentStage.PlayerPoint );
            _playerCannon.transform.position = _currentStage.PlayerPoint;

            UpdateLabel();

            MapManager.Instance.SetTilemap(_currentStage.GroundTilemap);
            
            //박스 갯수 셋팅
            //UI 연결

            StartCoroutine(DelayStart());
        }
        else
        {
            Debug.Log($"There is no stage level : {level.ToString()}");
        }
    }

    public void DecreaseBallAndBoxCount(int ballCnt, int boxCnt)
    {
        _currentBallCount -= ballCnt;
        _currentBoxCount -= boxCnt;
        UpdateLabel();

        if(_currentBallCount <= 0 || _currentBoxCount <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        TimeController.Instance.StopTimeScale();
        OnGameOver?.Invoke(_currentBoxCount, _currentBallCount);
    }

    private void UpdateLabel()
    {
        OnChangeLabel?.Invoke(LabelCategory.Ball,
                        $"{_currentBallCount.ToString()} / {_currentStage.BallCount.ToString()}");
        OnChangeLabel?.Invoke(LabelCategory.Box,
                                $"{_currentBoxCount.ToString()} / {_currentStage.TotalBoxCount.ToString()}");
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(1f); //1초 대기후 스테이지 시작
        OnLoadStageComplete?.Invoke();
    }

    #region 디버그 코드
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameOver();
        }
    }
    #endregion
}