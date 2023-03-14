using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWeapon : MonoBehaviour
{
    private float desireAngle; // ���Ⱑ �ٶ�����ϴ� ����

    protected WeaponRenderer weaponRenderer;
    protected Weapon weapon;

    protected virtual void Awake()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Weapon>();
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; // ���콺 ���� ����

        desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg; // ��׸� ����
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(desireAngle, Vector3.forward); // z�� ���� ȸ��
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
