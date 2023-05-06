using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private Image fillImage;
    private TextMeshProUGUI angleTxt;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("다수의 UIManager 감지");
        }
        Instance = this;
        fillImage = transform.Find("BottomPanel/PowerIndicator/GaugeMask/FillBar").GetComponent<Image>();   
        angleTxt = transform.Find("BottomPanel/PowerIndicator/AngleText").GetComponent<TextMeshProUGUI>();   
    }

    public void FillGaugeBar(float curPower, float maxPower)
    {
        fillImage.fillAmount = curPower / maxPower;
    }

    public void SetAngleText(float angle)
    {
        angleTxt.SetText($"{Mathf.FloorToInt(angle)} Degree");
    }
}
