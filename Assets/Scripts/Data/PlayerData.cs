using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class PlayerData
{
    public int currentStage = 1;

    public double baseAttack;
    public float baseCritChance;
    public float baseCritDamage;
    public float baseGoldBonus;

    public int atkUpgradLevel;
    public int critChanceUpgradeLevel;
    public int critDamageUpgradeLevel;
    public int goldBonusUpgradeLevel;
    public int autoAttackSpeedUpgradeLevel;

    public string equippedWeaponId = "";

}
