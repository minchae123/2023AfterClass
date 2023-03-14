using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;
    [SerializeField] protected Transform muzzle; // �ѱ� ��ġ
    [SerializeField] protected Transform shellEjectPosition; // ź�� ���� ��ġ

    public WeaponDataSO WeaponData => weaponData; // ���߿� ������ �� �� �ְ� ����

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    protected bool isShooting; // �߻� �� ?
    protected bool delayCoroutine = false;

    #region AMMO ����
    protected int ammo;
    protected int Ammo
    {
        get { return ammo; }
        set
        {
            ammo = Math.Clamp(value, 0, weaponData.ammoCapacity);
        }
    }
    public bool AmmoFull => Ammo == weaponData.ammoCapacity;
    public int EmptyBulletCnt => weaponData.ammoCapacity - ammo; // ���� ������ źȯ��
    #endregion


    private void Awake()
    {
        ammo = weaponData.ammoCapacity;
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        // �Ѿ� �߻� ��� ������ ������ �߻�
        if (isShooting && delayCoroutine == false)
        {
            if (ammo > 0)
            {
                OnShoot?.Invoke();
                for(int i = 0; i < weaponData.bulletCount; i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishOneShot();
        }
    }

    private void FinishOneShot()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(weaponData.autoFire == false)
        {
            isShooting = false;
        }
    }

    private IEnumerator DelayNextShootCoroutine()
    {
        delayCoroutine = true;
        yield return new WaitForSeconds(weaponData.weaponDelay);
        delayCoroutine = false;
    }

    private void ShootBullet()
    {
        Debug.Log("����");
    }

    public void TryShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
        OnStopShooting?.Invoke();
    }
}
