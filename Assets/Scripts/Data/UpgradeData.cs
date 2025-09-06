using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade System/Upgrade Data")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public int baseCost;
    public float baseValue;
    public float valueIncreasePerLevel;
    public float costIncreasePerLevel;
}