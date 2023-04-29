using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : EnemyAttack
{
    [SerializeField] private FireBall enemyBulletPrefab;
    [SerializeField] private float coolTime = 3f;
    private float lastFireTime = 0;
    private FireBall curFireBall = null;

    public override void Attack()
    {
        if (actionData.isAttack == false && lastFireTime + coolTime < Time.time)
        {
            actionData.isAttack = true;

            StartAttackSequence();
        }
    }
    
    private void StartAttackSequence()
    {
        Sequence seq = DOTween.Sequence();

        curFireBall = PoolManager.Instance.Pop("FireBall") as FireBall;
        curFireBall.transform.position = transform.position + new Vector3(0, 0.25f, 0f);
        curFireBall.transform.localScale = Vector3.one * 0.1f;
        seq.Append(curFireBall.transform.DOMoveY(curFireBall.transform.position.y + 1f, 0.5f));
        seq.Join(curFireBall.transform.DOScale(Vector3.one * 0.4f, 0.5f));
        seq.Append(curFireBall.transform.DOScale(Vector3.one, 1.2f));
        var t = DOTween.To(
            () => curFireBall.Light.intensity,
            value => curFireBall.Light.intensity = value,
            curFireBall.lightMaxIntensity,
            1.2f);
        seq.Join(t);

        seq.AppendCallback(() =>
        {
            lastFireTime = Time.time;
            actionData.isAttack = false;
            curFireBall.Fire(curFireBall.transform.right);

            curFireBall = null;
        });
    }

    public void FaceDirection(Vector2 pointerInput)
    {

        if (curFireBall == null) return;
        Vector3 direction = (Vector3)pointerInput - curFireBall.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        curFireBall.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
