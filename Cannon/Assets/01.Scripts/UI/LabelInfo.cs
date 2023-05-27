using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LabelInfo : MonoBehaviour
{
    private TextMeshProUGUI _textLabel;
    private Image _icon;
    private Vector2 _initAnchorPosition;
    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();
        _textLabel = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string value)
    {
       _textLabel.SetText(value);
    }

    public void SetText(int value, float time)
    {
        StartCoroutine(TextCoroutine(value, time));
    }

    private IEnumerator TextCoroutine(int value, float time)
    {
        float p = 0;
        float currentTime = 0;
        while (p < 1)
        {
            currentTime += Time.unscaledDeltaTime;
            p = currentTime / time;
            int v = Mathf.CeilToInt(Mathf.Lerp(0, value, p));

            _textLabel.SetText(v.ToString());
            yield return null;
        }
        _textLabel.SetText(value.ToString());
    }

    public void Init()
    {
        RectTransform rectTrm = GetComponent<RectTransform>(); //?еш? rect

        _initAnchorPosition = _icon.rectTransform.anchoredPosition;
        _icon.rectTransform.anchoredPosition = new Vector2(0, rectTrm.sizeDelta.y);
        _textLabel.SetText("0");
    }

    public Tween SetIcon(float time)
    {
        return _icon.rectTransform.DOAnchorPos(_initAnchorPosition, time);
    }
}
