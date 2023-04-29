using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeapon : MonoBehaviour
{
    private float desireAngle; //���Ⱑ �ٶ���� �ϴ� ����

    protected WeaponRenderer weaponRenderer;
    protected Weapon weapon;

    public UnityEvent<int, int> OnChangeTotalAmmo;
    [SerializeField] private ReloadGaugeUI reloadUI = null;
    [SerializeField] private AudioClip cannotClip = null;

    [SerializeField] private int maxTotalAmmo = 9999, totalAmmo = 300; // �ִ� 9999�� ���� 300��

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
        weapon = GetComponentInChildren<Weapon>(); //�ڱⰡ ��� �ִ� �� ��������

        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        OnChangeTotalAmmo?.Invoke(weapon.Ammo, totalAmmo);
    }

    #region ���ε� ���� ����
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

        OnChangeTotalAmmo?.Invoke(weapon.Ammo, totalAmmo); // ���� ���� źâ ���� ���� ���� źȯ��

        isReloading = false;
    }
    #endregion


    public void AddAmmo(int count)
    {
        totalAmmo += count;
    }

    public virtual void AimWeapon(Vector2 pointerPos)
    {
        Vector3 aimDirection = (Vector3)pointerPos - transform.position; //���콺 ���� ���͸� ���ϰ�
        
        desireAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        //��׸� ������ ���Ѵ�.

        AdjustWeaponRendering();

        transform.rotation = Quaternion.AngleAxis(desireAngle, Vector3.forward);
        //z�� �������� ȸ��
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
        //�ڱⰡ ���⸦ ��� ���� ��� �߻�
        if (isReloading) return; // ���ε����� �߻� ����
        weapon?.TryShooting();
    }

    public virtual void StopShooting()
    {
        weapon?.StopShooting();
    }
}
