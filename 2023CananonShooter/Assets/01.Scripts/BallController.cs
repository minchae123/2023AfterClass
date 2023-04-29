using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Transform ArrowTrm;

    private Vector2 _keyInput;

    [SerializeField]
    private float _maxPower = 15f, _chargeSpeed = 20f;
    private float _currentPower = 0;

    private Rigidbody2D _rigid;
    private SpriteRenderer arrowRenderer;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        arrowRenderer = transform.Find("Direction").GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        _keyInput = worldPos - transform.position;
        float angle = Mathf.Atan2(_keyInput.y, _keyInput.x);
        ArrowTrm.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);

        if(Input.GetKey(KeyCode.Space))  //Ű�� ������ ���� ��
        {
            _currentPower += _chargeSpeed * Time.deltaTime;
            _currentPower = Mathf.Clamp(_currentPower, 0, _maxPower);
        }

        if (Input.GetKeyUp(KeyCode.Space))  //Ű�� ������ ��
        {
            Fire();
        }

        //PowerBarTrm.localScale = new Vector3(_currentPower / _maxPower, 1, 1);

        float x = 1 + (_currentPower / _maxPower) * 3;
        arrowRenderer.size = new Vector2(x, 1f);
    }
    
    private void Fire()
    {
        _rigid.velocity = Vector2.zero; //���ݼӵ� �ʱ�ȭ�Ŀ� 
        _rigid.AddForce(_keyInput.normalized * _currentPower, ForceMode2D.Impulse);
        _currentPower = 0;
    }
    
    //���� �������� �ִ��� �����ҷ��� �ϴ� �ֶ� ������ ���������� ���⼭ ����� �Ѵ�.
    //private void FixedUpdate()
    //{
    //    Vector2 velocity = _rigid.velocity;
    //    velocity.x = _xInput * _speed;
    //    _rigid.velocity = velocity;
    //}
}
