using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Image _fillImage;
    private TextMeshProUGUI _angleText;
    private TextMeshProUGUI _msgText;

    private RectTransform _cannonThumbnail;
    private bool _isShowThumbnail;
    
    private Transform _playerTrm;
    private Camera _mainCam;  

    private LoadingScreen _loadingScreen;
    private InfoPanel _infoPanel;
    private ResultPanel _resultPanel;

    public void Init(GameManager manager)
    {
        _playerTrm = manager.PlayerTrm;
        
        _fillImage = transform.Find("BottomPanel/PowerIndicator/GaugeMask/FillBar").GetComponent<Image>();
        _angleText = transform.Find("BottomPanel/PowerIndicator/AngleText").GetComponent<TextMeshProUGUI>();

        _msgText = transform.Find("BottomPanel/MessageText").GetComponent<TextMeshProUGUI>();
        _cannonThumbnail = transform.Find("CannonThumbnail").GetComponent<RectTransform>(); 
        _mainCam = Camera.main;

        _loadingScreen = transform.Find("LoadingImage").GetComponent<LoadingScreen>();
        manager.OnLoadStageStart += _loadingScreen.LoadingOn; //로딩이 시작되면 로딩화면 나오도록
        manager.OnLoadStageComplete += _loadingScreen.LoadingOff; //로딩이 끝나면 로딩스크린 사라지는걸로

        _infoPanel = transform.Find("InfoPanel").GetComponent<InfoPanel>(); //정보패널
        manager.OnChangeLabel += _infoPanel.SetInfoText; //구독

        _resultPanel = transform.Find("ResultPanel").GetComponent<ResultPanel>();
        manager.OnGameOver += _resultPanel.ShowProgress;
    }

    public void FillGaugeBar(float _currentPower, float _maxPower)
    {
        _fillImage.fillAmount = _currentPower / _maxPower;
    }

    public void SetAngleText(float angle)
    {
        _angleText.SetText($"{ Mathf.FloorToInt(angle) } Degree");
    }

    public void ShowText(string msg)
    {
        _msgText.alpha = 1;
        _msgText.SetText(msg);

        _msgText.DOFade(0.4f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void HideText()
    {
        _msgText.DOKill(); 
        _msgText.alpha = 0;
    }

    public void ShowThumbnail(bool value)  //7
    {
        _cannonThumbnail.DOKill();
        if(value)
        {
            _cannonThumbnail.DOAnchorPosX(20, 0.8f);
        }else
        {
            _cannonThumbnail.DOAnchorPosX(-200, 0.8f);
        }
    }


    private void Update()  //8
    {
        float halfWidth = _mainCam.orthographicSize * _mainCam.aspect;
        if(Mathf.Abs(_playerTrm.position.x - _mainCam.transform.position.x) + 1f  > halfWidth && _isShowThumbnail == false)
        {
            _isShowThumbnail = true;
            ShowThumbnail(true);
        }else if(Mathf.Abs(_playerTrm.position.x - _mainCam.transform.position.x) + 1f < halfWidth && _isShowThumbnail == true)
        {
            _isShowThumbnail = false;
            ShowThumbnail(false);
        }
    }
}
