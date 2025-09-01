using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatData", menuName = "GameData/WeaponStatData")]
public class WeaponStats : ScriptableObject
{
    public EnforceLevel[] stats;    // ��ȭ�� ����

    public EnforceLevel GetEnforceLevel(int level)  // ��ȭ�������� ��������
    {
        if (level > 5) level = 5;

        return stats[level];    // 0������ 5������
    }
}