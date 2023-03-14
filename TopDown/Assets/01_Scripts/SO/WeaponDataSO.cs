using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/Weapon/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [Range(0, 999)] public int ammoCapacity = 100; // źâũ��
    public bool autoFire; // ������ �ִ� ���� ���� ? �ܹ� ?

    [Range(0.1f, 2f)] public float weaponDelay = 0.1f; // ��� �����ð�
    [Range(0f, 10f)] public float spreadAngle = 5f; // ź�� ������ ����

    public int bulletCount = 1;
}
