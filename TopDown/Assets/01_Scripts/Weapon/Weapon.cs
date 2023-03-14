using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponDataSO weaponData;
    [SerializeField] protected Transform muzzle; // 총구 위치
    [SerializeField] protected Transform shellEjectPosition; // 탄피 배출 위치

    public WeaponDataSO WeaponData => weaponData; // 나중에 가져다 쓸 수 있게 겟터

    public UnityEvent OnShoot;
    public UnityEvent OnShootNoAmmo;
    public UnityEvent OnStopShooting;
    protected bool isShooting; // 발사 중 ?
    protected bool delayCoroutine = false;

    #region AMMO 관련
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
    public int EmptyBulletCnt => weaponData.ammoCapacity - ammo; // 현재 부족한 탄환수
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
        // 총알 발사 명령 딜레이 없으면 발사
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
        Debug.Log("빵야");
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
