using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    private float desireAngle; // 무기가 바라봐야하는 방향

    protected WeaponRenderer weaponRenderer;
    protected Weapon weapon;

    protected virtual void Awake()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; // 마우스 방향 벡터

        desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; // 디그리 각도
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(desireAngle, Vector3.forward); // z축 기준 회전
    }

    private void AdjustWeaponRendering()
    {
        if(weaponRenderer != null)
        {
            weaponRenderer.FlipSprite(desireAngle > 90f || desireAngle < -90f);
            weaponRenderer.RenderBehindHead(desireAngle > 0 && desireAngle < 180);
        }
    }

    public virtual void Shoot()
    {
        weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        weapon?.StopShooting();
    }
}
