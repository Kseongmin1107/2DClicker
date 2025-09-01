using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats statData;
    private int level;

    public void Enforce()
    {
        level++;
        if (level > 5)
            level = 5;
        var stat = statData.GetEnforceLevel(level);

        Debug.Log($"���ݷ�:{stat.attackPower}, ġ��Ÿ:{stat.criticalRate}, ���ݼӵ�:{stat.attackSpeed}");
    }
}