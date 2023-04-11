using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class PopupText : PoolableMono
{
    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Setup(string text, Vector3 pos, Color color, float fontSize = 7f)
    {
        transform.position = pos;
        textMesh.SetText(text);
        textMesh.color = color;
        textMesh.fontSize = fontSize;
    }

    private void ShowingSequence()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(transform.position.y + 0.5f, 1f));
        seq.Join(textMesh.DOFade(0, 1f));
        seq.AppendCallback(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }

    public override void Reset()
    {
        textMesh.color = Color.white;
        textMesh.fontSize = 7f;
        textMesh.alpha = 1;
    }
}
