using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
    public Transform Target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged; //마우스방향전환을 
    public UnityEvent OnAttackButtonPress = null;

    public Transform BasePosition; //이게 거리측정을 몬스터의 바닥에서 

    public AIState CurrentState;
    
    private EnemyRenderer enemyRenderer;
    [SerializeField] private bool isActive = false;
    
    private void Awake()
    {
        enemyRenderer = transform.Find("VisualSprite").GetComponent<EnemyRenderer>();    
    }

    private void Start()
    {
        Target = GameManager.Instance.PlayerTrm;
        CurrentState?.SetUp(transform);
    }

    public void ChangeState(AIState nextState)
    {
        CurrentState = nextState;
        CurrentState?.SetUp(transform); //이 부분은 최적화 필요해
    }

    public void Update()
    {
        if (isActive == false)
            return;

        if(Target == null)
        {
            OnMovementKeyPress?.Invoke(Vector2.zero);
        }
        else
        {
            CurrentState.UpdateState(); //현재 상태를 갱신한다.
        }
    }

    public void ShowEnemy()
    {
        isActive = false;
        enemyRenderer.ShowProgress(2f, () => { isActive = true; });
    }

    public void Move(Vector2 dir, Vector2 pos)
    {
        OnMovementKeyPress?.Invoke(dir);
        OnPointerPositionChanged?.Invoke(pos);
    }

    public void Attack()
    {
        OnAttackButtonPress?.Invoke();
    }

    public override void Reset()
    {
        isActive = false;
        enemyRenderer.Reset();
    }
}
