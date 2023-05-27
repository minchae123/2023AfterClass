using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private RectTransform _rectTrm;
    private Image _rectImage; 
    private Vector2 _screenSize;
    private void Awake()
    {
        _rectTrm = GetComponent<RectTransform>();
        _rectImage = GetComponent<Image>();
        _screenSize = new Vector2(Screen.width, Screen.height);
        _rectTrm.sizeDelta = _screenSize;
        _rectTrm.anchoredPosition = new Vector2(0, 0);
    }

    public void LoadingOff()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosY(_screenSize.y, 0.5f).SetEase(Ease.OutCubic));
        seq.Join(_rectImage.DOFade(0, 0.5f));
    }

    public void LoadingOn()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(_rectTrm.DOAnchorPosY(0, 0.5f).SetEase(Ease.InCubic));
        seq.Join(_rectImage.DOFade(1, 0.5f));
    }

}
