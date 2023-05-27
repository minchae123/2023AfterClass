using System;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    [SerializeField]
    private LayerMask _whatIsTarget;
    [SerializeField]
    private float _expRadius = 2.5f;

    [SerializeField]
    private Explosion _expPrefab;
    [SerializeField]
    private float _lifeTime = 3f;
    private float _currentLifeTime = 0;

    public event Action OnExplosion;  //�̰� �� ĳ�� �ȿ����� ȣ�� �����ϴ�.

    private bool isActive = true;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Fire(Vector2 direction, float power)
    {
        _rigidbody.AddForce(direction * power, ForceMode2D.Impulse);
    }

    private void Update()
    {
        _currentLifeTime += Time.deltaTime;
        if(_currentLifeTime >= _lifeTime)
        {
            GameManager.Instance.DecreaseBallAndBoxCount(1, 0);
            DestroyBall();
        }
    }

    private void DestroyBall()
    {
        OnExplosion?.Invoke();

        Explosion exp = Instantiate(_expPrefab, transform.position, Quaternion.identity);
        exp.PlayParticle();

        CameraManager.Instance.StartShakee(2.5f, 0.6f);

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActive == false) return;

        isActive = false;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _expRadius, _whatIsTarget);

        float maxPower = 5f;
        float minPower = 2f;

        foreach(Collider2D c in cols)
        {
            Box box = c.GetComponent<Box>();

            Vector2 dir = c.transform.position - transform.position;
            //�Ÿ��� �ݺ���ؼ� �Ÿ��� ������5f �־����� 2f�� ���������� ���������� �ϵ� 2f�����δ� ���� �ȶ�������

            float percent = dir.magnitude / _expRadius;

            float power = Mathf.Lerp(maxPower, minPower, percent);

            box.DestroyBox(dir.normalized * power + new Vector2(0, 4f));
        }

        GameManager.Instance.DecreaseBallAndBoxCount(1, cols.Length);

        TimeController.Instance.SetTimeScale(0.2f, 0.1f, () =>
        {
            TimeController.Instance.SetTimeScale(1, 0.3f);
        });

        MapManager.Instance.SetExplosion(transform.position, _expRadius);

        DestroyBall();
    }
}
