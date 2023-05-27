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

    public event Action OnExplosion;  //이건 이 캐논볼 안에서만 호출 가능하다.

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
            //거리에 반비례해서 거리가 가까우면5f 멀어지면 2f로 점진적으로 낮아지도록 하되 2f밑으로는 힘이 안떨어지게

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
