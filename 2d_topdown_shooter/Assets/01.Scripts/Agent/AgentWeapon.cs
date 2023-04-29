using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    private float desireAngle; //무기가 바라봐야 하는 방향

    protected WeaponRenderer weaponRenderer;
    protected Weapon weapon;

    public UnityEvent<int, int> OnChangeTotalAmmo;
    [SerializeField] private ReloadGaugeUI reloadUI = null;
    [SerializeField] private AudioClip cannotClip = null;

    [SerializeField] private int maxTotalAmmo = 9999, totalAmmo = 300; // 최대 9999발 최초 300발

    public int TotalAmmo { 
        get => totalAmmo;
        set
        {
            totalAmmo = value;
            totalAmmo = Mathf.Clamp(totalAmmo, 0, maxTotalAmmo);
            OnChangeTotalAmmo?.Invoke(weapon.Ammo, maxTotalAmmo);
        }
    }

    private AudioSource audioSource;
    private bool isReloading = false;
    public bool IsReloading => isReloading;

    protected virtual void Awake()
    {
        weaponRenderer = GetComponentInChildren<WeaponRenderer>();
        weapon = GetComponentInChildren<Weapon>(); //자기가 들고 있는 총 가져오기

        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        OnChangeTotalAmmo?.Invoke(weapon.Ammo, totalAmmo);
    }

    #region 리로딩 관련 로직
    public void Reload()
    {
        if (isReloading == false && totalAmmo > 0 && weapon.AmmoFull == false)
        {
            isReloading = true;
            weapon.StopShooting();
            StartCoroutine(ReloadCoroutine());
        }
        else
        {
            PlayClip(cannotClip);
        }
    }

    private void PlayClip(AudioClip cannotClip)
    {
        audioSource.Stop();
        audioSource.clip = cannotClip;
        audioSource.Play();
    }


    IEnumerator ReloadCoroutine()
    {
        reloadUI.gameObject.SetActive(true);
        float time = 0;
        while(time <= weapon.WeaponData.reloadTime)
        {
            time += Time.deltaTime;
            reloadUI.ReloadGaugeNormal(time / weapon.WeaponData.reloadTime);
            yield return null;
        }

        reloadUI.gameObject.SetActive(false);
        if(weapon.WeaponData.reloadClip != null)
            PlayClip(weapon.WeaponData.reloadClip);

        int reloadedAmmo = Mathf.Min(totalAmmo, weapon.EmptyBulletCnt);
        totalAmmo -= reloadedAmmo;
        weapon.Ammo += reloadedAmmo;

        OnChangeTotalAmmo?.Invoke(weapon.Ammo, totalAmmo); // 현재 총의 탄창 수와 내가 가진 탄환수

        isReloading = false;
    }
    #endregion


    public void AddAmmo(int count)
    {
        totalAmmo += count;
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; //마우스 방향 벡터를 구하고
        
        desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //디그리 각도를 구한다.

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(desireAngle, Vector3.forward);
        //z축 기준으로 회전
    }

    private void AdjustWeaponRendering()
    {
        if(weaponRenderer != null)
        {
            weaponRenderer.FlipSprite(desireAngle > 90f || desireAngle < -90f);
            weaponRenderer.RenderBehindHead(  desireAngle > 0 && desireAngle < 180 );
        }
    }

    public virtual void Shoot()
    {
        //자기가 무기를 들고 있을 경우 발사
        if (isReloading) return; // 리로딩에는 발사 금지
        weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        weapon?.StopShooting();
    }
}
