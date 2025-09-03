using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatData", menuName = "GameData/WeaponStatData")]
public class WeaponStats : ScriptableObject
{
    public string[] weaponName;
    public EnhanceLevel[] stats;    // ��ȭ�� ����

    public float baseAttackPower;
    public float baseCriticalRate;  // ���⺰ ���̽� ����

    public string GetWeaponName(int level)
    {
        int nameIndex = level / 6;  // 0~5�� �̸�1, �״��� �̸�2
        if (nameIndex >= weaponName.Length)
            nameIndex = weaponName.Length - 1;      // �ִ� ���Ŀ��� �ȳѾ��
        return weaponName[nameIndex];
    }

    public EnhanceLevel GetEnhanceLevel(int level)  // ��ȭ�������� ��������
    {
        if (level >= stats.Length)
            level = stats.Length - 1;

        return stats[level];    // 0������ 5������
    }
}