using UnityEngine;
using TMPro;
using System.Collections;

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

    private Coroutine autoUpgradeCoroutine;


    public AutoAttack autoAttack;
    private void Start()
    {
        currentLevel = 0;
        UpdateUpgradeUI();
       // UpdateFinalStat();
    }
    public void StartAutoUpgrade()
    {
        if (autoUpgradeCoroutine == null)
        {
            autoUpgradeCoroutine = StartCoroutine(AutoUpgradeLoop());
        }
    }

    public void StopAutoUpgrade()
    {
        if (autoUpgradeCoroutine != null)
        {
            StopCoroutine(autoUpgradeCoroutine);
            autoUpgradeCoroutine = null;
        }
    }
    private IEnumerator AutoUpgradeLoop()
    {
        UpgradeAndCheckCost();

        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            UpgradeAndCheckCost();
        }
    }
    private void UpgradeAndCheckCost()
    {
        // 골드나 비용이 충분한지 확인하는 로직 추가
        // if (GameManager.Instance.gold >= nextLevelCost)
        // {
        //     GameManager.Instance.SpendGold(nextLevelCost); // 골드 소모
        currentLevel++;
        UpdateUpgradeUI();
        //     UpdateFinalStat();
        // }
    }

    public void OnClickUpgradeButton()
    {
        UpgradeAndCheckCost();
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
           //     if (autoAttack != null)
           //     {
           //         autoAttack.UpdateAutoAttackSpeed(currentValue);
           //     }
           //     break
    //        case StatType.GoldBonus:
    //            GameManager.Instance.finalGoldBonus = currentValue;
    //            break;
    //    }
    //}
}