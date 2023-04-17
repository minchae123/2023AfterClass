using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBrain : PoolableMono
{
    public Transform Target;

    public UnityEvent<Vector2> OnMovementKeyPress;
    public UnityEvent<Vector2> OnPointerPositionChanged; //���콺������ȯ�� 
    public UnityEvent OnAttackButtonPress = null;

    public Transform BasePosition; //�̰� �Ÿ������� ������ �ٴڿ��� 

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
        CurrentState?.SetUp(transform); //�� �κ��� ����ȭ �ʿ���
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
            CurrentState.UpdateState(); //���� ���¸� �����Ѵ�.
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
