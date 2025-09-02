using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatData", menuName = "GameData/WeaponStatData")]
public class WeaponStats : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public EnhanceLevel[] stats;    // ��ȭ�� ����

    public EnhanceLevel GetEnhanceLevel(int level)  // ��ȭ�������� ��������
    {
        if (level > 5) level = 5;

        return stats[level];    // 0������ 5������
    }
}