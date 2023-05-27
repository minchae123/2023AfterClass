using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResultPanel : MonoBehaviour
{
    private Dictionary<LabelCategory, LabelInfo> _labelDictionary;
    private RectTransform rectTrm;

    private void Awake()
    {
        rectTrm = GetComponent<RectTransform>();
        rectTrm.anchoredPosition = new Vector2(0, Screen.height); // 앵커 포지션 위로
        _labelDictionary = new Dictionary<LabelCategory, LabelInfo>();

        foreach (LabelCategory cat in Enum.GetValues(typeof(LabelCategory)))
        {
            LabelInfo info = transform.Find($"{cat.ToString()}CountBox").GetComponent<LabelInfo>();
            info.Init();
            _labelDictionary.Add(cat, info);
        }
    }

    public void SetInfoText(LabelCategory cat, string value)
    {
        if (_labelDictionary.TryGetValue(cat, out var info))
        {
            info.SetText(value);
        }
    }

    public void ShowProgress(int boxCount, int ballCount)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(rectTrm.DOAnchorPos(Vector2.zero, 0.8f));
        seq.Append(_labelDictionary[LabelCategory.Ball].SetIcon(0.6f));
        seq.Join(_labelDictionary[LabelCategory.Box].SetIcon(0.6f));

        seq.AppendCallback(() =>
        {
            _labelDictionary[LabelCategory.Ball].SetText(ballCount, 0.7f);
        });
        seq.AppendCallback(() =>
        {
            _labelDictionary[LabelCategory.Box].SetText(boxCount, 0.7f);
        });
        seq.SetUpdate(true);
    }
}
