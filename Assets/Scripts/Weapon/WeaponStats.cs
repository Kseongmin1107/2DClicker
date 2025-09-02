using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatData", menuName = "GameData/WeaponStatData")]
public class WeaponStats : ScriptableObject
{
    public string[] weaponName;
    public EnhanceLevel[] stats;    // 강화별 스탯

    public string GetWeaponName(int level)
    {
        int nameIndex = level / 6;  // 0~5강 이름1, 그다음 이름2
        if (nameIndex >= weaponName.Length)
            nameIndex = weaponName.Length - 1;
        return weaponName[nameIndex];
    }

    public EnhanceLevel GetEnhanceLevel(int level)  // 강화레벨정보 가져오기
    {
        if (level > 5) level = 5;

        return stats[level];    // 0강부터 5강까지
    }
}