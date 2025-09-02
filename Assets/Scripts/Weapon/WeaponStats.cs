using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatData", menuName = "GameData/WeaponStatData")]
public class WeaponStats : ScriptableObject
{
    public string weaponName;
    public GameObject weaponPrefab;
    public EnhanceLevel[] stats;    // 강화별 스탯

    public EnhanceLevel GetEnhanceLevel(int level)  // 강화레벨정보 가져오기
    {
        if (level > 5) level = 5;

        return stats[level];    // 0강부터 5강까지
    }
}