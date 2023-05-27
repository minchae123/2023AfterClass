using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LabelCategory
{
    Ball = 1,
    Box = 2,
}

public class InfoPanel : MonoBehaviour
{
    private Dictionary<LabelCategory, LabelInfo> _labelDictionary;

    private void Awake()
    {
        _labelDictionary = new Dictionary<LabelCategory, LabelInfo>();

        foreach (LabelCategory cat in Enum.GetValues( typeof(LabelCategory) ))
        {
            LabelInfo info = transform.Find($"{cat.ToString()}Label").GetComponent<LabelInfo>();
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
}
