using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    public enum StatType { CriticalDamage, AutoAttack, GoldBonus }

    [Header("Upgrade Settings")]
    [SerializeField] private StatType statType;
    [SerializeField] private UpgradeData upgradeData;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI costText;

    private int currentLevel;
    private float currentValue;
    private int nextLevelCost;

    private void Start()
    {
        currentLevel = 0;
        UpdateUpgradeUI();
       // UpdateFinalStat();
    }

    public void OnClickUpgradeButton()
    {
        currentLevel++;
        UpdateUpgradeUI();
       // UpdateFinalStat();
    }

    public void UpdateUpgradeUI()
    {
        // 현재 레벨에 따른 능력치 값 계산
        currentValue = upgradeData.baseValue + (currentLevel * upgradeData.valueIncreasePerLevel);

        // 다음 레벨업에 필요한 비용 계산
        nextLevelCost = upgradeData.baseCost + (currentLevel * (int)upgradeData.costIncreasePerLevel);

        // UI 텍스트 갱신
        levelText.text = upgradeData.upgradeName +" " + currentLevel.ToString();
        switch (statType)
        {
            case StatType.CriticalDamage:
            case StatType.GoldBonus:
                valueText.text = "+" + currentValue.ToString("F1") + "%";
                break;
            case StatType.AutoAttack:
                valueText.text = currentValue.ToString("F1") + "회/초";
                break;
        }

        costText.text = nextLevelCost.ToString();
    }
    //게임매니저 스크립트 생성후 주석 해제
        //private void UpdateFinalStat()
    //{
    //    if (GameManager.Instance == null)
    //    {
    //        Debug.LogError("GameManager.Instance is not found!");
    //        return;
    //    }

    //    switch (statType)
    //    {
    //        case StatType.CriticalDamage:
    //            GameManager.Instance.finalCriticalDamage = currentValue;
    //            break;
           //case StatType.AutoAttack:
   //             AutoAttackController.Instance.UpdateAttackSpeed(currentValue);
   //             break;
    //        case StatType.GoldBonus:
    //            GameManager.Instance.finalGoldBonus = currentValue;
    //            break;
    //    }
    //}
}