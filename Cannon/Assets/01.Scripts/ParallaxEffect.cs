using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Vector2 _parallaxRatio; 
    //카메라의 이동량을 몇퍼 반영할껀가?

    private Transform _mainCamTrm;
    private Vector3 _lastCamPos;

    private float _textureUnitSizeX;

    private bool _active = false;
    void Start()
    {
        GameManager.Instance.OnLoadStageComplete += StartParallax;
    }

    private void StartParallax()
    {
        _mainCamTrm = Camera.main.transform;
        _lastCamPos = _mainCamTrm.position; //마지막 카메라 포지션

        Sprite s = GetComponent<SpriteRenderer>().sprite;
        Texture2D tex = s.texture; //스프라이트 렌더러에 들어가있는 스프라이트의 텍스쳐를 알아내서 가져온다. 
        //이건 반드시 통짜 이미지만 가능하다.  rect를 가져와서 진짜 width pixel 

        _textureUnitSizeX = tex.width / s.pixelsPerUnit; //이 텍스쳐가 몇 유닛짜리인지 계산한다.

        _active = true;
    }

    private void LateUpdate()
    {
        if(_active == false) return;
        //카메라 이동이 이루어지즌 Update다음에 실행을 해야 한다.
        Vector3 deltaMove = _mainCamTrm.position - _lastCamPos; //현재 프레임에 이동한 양을 따져서
        transform.Translate(
            new Vector3(deltaMove.x * _parallaxRatio.x, deltaMove.y * _parallaxRatio.y),
            Space.World
            );
        _lastCamPos = _mainCamTrm.position; //마지막 카메라 포지션을 갱신해준다.

        if (Mathf.Abs(_mainCamTrm.position.x - transform.position.x) >= _textureUnitSizeX)
        {
            Vector3 pos = transform.position;
            float offsetX = (_mainCamTrm.position.x - pos.x) % _textureUnitSizeX;
            transform.position = new Vector3(_mainCamTrm.position.x - offsetX, pos.y);
        }
    }
}
