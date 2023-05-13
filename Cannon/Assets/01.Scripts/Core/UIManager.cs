using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Image fillImage;
    private TextMeshProUGUI angleTxt;
    private TextMeshProUGUI msgTxt;

    private RectTransform thumbnail;
    private bool isShowThumbnail; //2
    private Camera _mainCam;  //4
    [SerializeField] private Transform playerTrm; //3

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("다수의 UIManager 감지");
        }
        Instance = this;
        fillImage = transform.Find("BottomPanel/PowerIndicator/GaugeMask/FillBar").GetComponent<Image>();   
        angleTxt = transform.Find("BottomPanel/PowerIndicator/AngleText").GetComponent<TextMeshProUGUI>();
        msgTxt = transform.Find("BottomPanel/MessageText").GetComponent<TextMeshProUGUI>();
        thumbnail = transform.Find("CannonThumbnail").GetComponent<RectTransform>();
        _mainCam = Camera.main; //6
    }

    public void FillGaugeBar(float curPower, float maxPower)
    {
        fillImage.fillAmount = curPower / maxPower;
    }

    public void SetAngleText(float angle)
    {
        angleTxt.SetText($"{Mathf.FloorToInt(angle)} Degree");
    }

    public void ShowText(string msg)
    {
        msgTxt.alpha = 1;
        msgTxt.SetText(msg);

        msgTxt.DOFade(0.4f, 0.5f).SetLoops(-1, LoopType.Yoyo);
    }

    public void HideText()
    {
        msgTxt.DOComplete();
        msgTxt.alpha = 0;
    }

    public void ShowThumbnail(bool value)
    {
        thumbnail.DOKill();
        if(value)
            thumbnail.DOAnchorPosX(60f, 1);
        else
            thumbnail.DOAnchorPosX(-200, 1);
    }

    private void Update()  //8
    {
        float halfWidth = _mainCam.orthographicSize * _mainCam.aspect;
        if (Mathf.Abs(playerTrm.position.x - _mainCam.transform.position.x) + 1f > halfWidth && isShowThumbnail == false)
        {
            isShowThumbnail = true;
            ShowThumbnail(true);
        }
        else if (Mathf.Abs(playerTrm.position.x - _mainCam.transform.position.x) + 1f < halfWidth && isShowThumbnail == true)
        {
            isShowThumbnail = false;
            ShowThumbnail(false);
        }
    }
}
