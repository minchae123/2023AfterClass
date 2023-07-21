using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackManager : MonoBehaviour
{
    public BackData backData;
    private Camera mainCam;
    

    private void Start()
    {
        backData = Resources.Load<BackData>("BackSO");
        mainCam = Camera.main;


    }

    public void SetColor(BackData data)
    {
        mainCam.backgroundColor = backData.backColor;

        Text[] txt = FindObjectsOfType<Text>();
        foreach(Text t in txt)
        {
            t.color = data.fontColor;
        }


        GameObject backImage = GameObject.FindGameObjectWithTag("BackImage");
        if(backImage != null)
        {
            SpriteRenderer background = backImage.GetComponent<SpriteRenderer>();
            background.sprite = data.backImage;
        }
        else
        {
            Debug.LogWarning("back ¾ø´Ù");
        }
    }
}
