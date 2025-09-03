using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData
{
    public int currentStage = 1;
    public double gold = 0;

    public double Attack;
    public float CritChance;

    public int critDamageUpgradeLevel;
    public int goldBonusUpgradeLevel;
    public int autoAttackSpeedUpgradeLevel;

    public string equippedWeaponId = "";

}
