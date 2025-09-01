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
        // ���� ������ ���� �ɷ�ġ �� ���
        currentValue = upgradeData.baseValue + (currentLevel * upgradeData.valueIncreasePerLevel);

        // ���� �������� �ʿ��� ��� ���
        nextLevelCost = upgradeData.baseCost + (currentLevel * (int)upgradeData.costIncreasePerLevel);

        // UI �ؽ�Ʈ ����
        levelText.text = upgradeData.upgradeName +" " + currentLevel.ToString();
        switch (statType)
        {
            case StatType.CriticalDamage:
            case StatType.GoldBonus:
                valueText.text = "+" + currentValue.ToString("F1") + "%";
                break;
            case StatType.AutoAttack:
                valueText.text = currentValue.ToString("F1") + "ȸ/��";
                break;
        }

        costText.text = nextLevelCost.ToString();
    }
    //���ӸŴ��� ��ũ��Ʈ ������ �ּ� ����
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