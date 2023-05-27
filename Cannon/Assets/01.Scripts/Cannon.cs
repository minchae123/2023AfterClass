using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonState : short
{
    IDLE = 0,
    MOVING = 1,
    CHARGING = 2,
    FIRE = 3,
    WAITING = 4, //다 터지고 나서 스페이스바 입력을 기다리는 중
}

public class Cannon : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed; //회전속도
    private float _currentRotate; //현재 포신의 회전

    [SerializeField]
    private CannonBall _ballPrefab; //이걸 만들어서 쏠거다.
    private Transform _firePosition;

    [SerializeField]
    private float _chargeSpeed = 80f, _maxPower = 120f;
    private float _currentPower = 0;

    private Transform _barrelTrm;
    private Transform _camRigTrm;

    [SerializeField]
    private CannonState _currentState;

    private CameraRig _cameraRig;  //1
    public CameraRig CameraRigCompo => _cameraRig; //2

    private void Awake()
    {
        _currentState = CannonState.WAITING; //초기 IDLE상태로 만든다.
        _barrelTrm = transform.Find("CannonBarrel");
        _firePosition = transform.Find("CannonBarrel/FirePosition");
        _camRigTrm = transform.Find("CameraRig");
        _cameraRig = _camRigTrm.GetComponent<CameraRig>(); //리그캠을 가져와준다.  //3
    }

    public void Init()
    {
        GameManager.Instance.OnLoadStageComplete += () => _currentState = CannonState.IDLE;
    }

    private void Update()  //매 프레임 마다 실행 된다.
    {
        if((short)_currentState < 2)
        {
            CheckRotate();
        }
        
        CheckFire();

        CheckWait();
    }

    private void CheckWait()
    {
        if (_currentState != CannonState.WAITING) return;

        if(Input.GetButtonDown("Jump"))
        {
            StartCoroutine(ChangeToIdle());
        }
    }

    private IEnumerator ChangeToIdle()
    {
        //UI관련 뭔가를 다시 변경해야겠지
        UIManager.Instance.HideText(); //텍스트 다시 감춰두고
        CameraManager.Instance.SetActiveCam(CameraCategory.RigCam);
        yield return new WaitForSeconds(1f);
        _currentState = CannonState.IDLE;
    }

    private void CheckRotate()
    {
        float y = Input.GetAxisRaw("Vertical"); //위아래 키 입력  //  1
        _currentRotate += y * _rotateSpeed * Time.deltaTime;  //매 프레임마다 1초에 프레임이 330이 나온다면 

        //회전을 제한하는 코드를 여기다가 작성해보세요.
        _currentRotate = Mathf.Clamp(_currentRotate, 0, 90);
        _barrelTrm.rotation = Quaternion.Euler(0, 0, _currentRotate);

        UIManager.Instance.SetAngleText(_currentRotate);
    }

    private void CheckFire()
    {
        if(Input.GetButtonDown("Jump") && (short)_currentState < 2)
        {
            _currentState = CannonState.CHARGING;
            //변수가 이제 3개가 필요해, 어느방향으로 쏠꺼야? 어느정도의 힘으로 쏠꺼야?, 무엇을쏠꺼야
            _currentPower = 0;            
        }

        if (Input.GetButton("Jump") && _currentState == CannonState.CHARGING)
        {
            _currentPower += _chargeSpeed * Time.deltaTime;
            _currentPower = Mathf.Clamp(_currentPower, 0, _maxPower);
        }
        
        if (Input.GetButtonUp("Jump") && _currentState == CannonState.CHARGING)
        {
            _currentState = CannonState.FIRE;

            StartCoroutine(FireSequence());
        }

        UIManager.Instance.FillGaugeBar(_currentPower, _maxPower);
    }

    private IEnumerator FireSequence()
    {
        CameraManager.Instance.SetActiveCam(CameraCategory.CannonCam);
        CameraManager.Instance.SetFollowTarget(CameraCategory.BallCam, _barrelTrm);//발사가 되는 순간을 위해 미리 땡겨놓고
        yield return new WaitForSeconds(1.5f);

        _camRigTrm.localPosition = Vector3.zero;

        CannonBall ball = Instantiate(_ballPrefab, _firePosition.position, Quaternion.identity);
        CameraManager.Instance.SetActiveCam(CameraCategory.BallCam, ball.transform);

        ball.Fire(_firePosition.right, _currentPower);
        ball.OnExplosion += HandleAfterExplosion; //이걸 구독처리해서 볼이 터졌을 때 이걸 실행하게 해줘라.
    }

    private void HandleAfterExplosion()
    {
        //CameraManager.Instance.SetActiveCam(CameraCategory.RigCam); //이부분 변경되어야 해
        //_currentState = CannonState.IDLE; //나중에 이거는 UI상태로 변경할꺼다.
        _currentState = CannonState.WAITING;

        //UI에 뭔가 띄워야겠지?
        UIManager.Instance.ShowText("Press Space key to continue");
    }


}
