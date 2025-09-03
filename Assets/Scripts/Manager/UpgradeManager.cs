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

    [SerializeField] private Color affordableColor = Color.black;
    [SerializeField] private Color insufficientColor = Color.red;

    public AutoAttack autoAttack;

    private int currentLevel;
    private float currentValue;
    private int nextLevelCost;
    private Coroutine autoUpgradeCoroutine;

    private void Awake()
    {
        if (autoAttack == null)
        {
            autoAttack = FindObjectOfType<AutoAttack>();
        }
    }

    private void Start()
    {
        if (GameManager.Instance != null) //게임이 시작될 때 플레이어의 이전 상황 업그레이드 불러오기
        {
            switch (statType)
            {
                case StatType.CriticalDamage:
                    currentLevel = GameManager.Instance.Player.critDamageUpgradeLevel;
                    break;
                case StatType.AutoAttack:
                    currentLevel = GameManager.Instance.Player.atkUpgradLevel;
                    break;
                case StatType.GoldBonus:
                    currentLevel = GameManager.Instance.Player.goldBonusUpgradeLevel;
                    break;
            }
        }
        UpdateUpgradeData();
        UpdateUpgradeUI();
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
        if (GameManager.Instance == null)
        {
            Debug.LogError("게임매니저 인스턴스를 찾을 수 없습니다!");
            return;
        }

        if (GameManager.Instance.TrySpendGold(nextLevelCost))
        {
            currentLevel++;
            UpdateUpgradeData();
            UpdateUpgradeUI();
            UpdateFinalStat();
        }
        else
        {
            StopAutoUpgrade();
        }
    }

    public void OnClickUpgradeButton()
    {
        UpgradeAndCheckCost();
    }

    private void UpdateUpgradeData()
    {
        currentValue = upgradeData.baseValue + (currentLevel * upgradeData.valueIncreasePerLevel);
        nextLevelCost = upgradeData.baseCost + (currentLevel * (int)upgradeData.costIncreasePerLevel);

        if (GameManager.Instance != null)
        {
            switch (statType)
            {
                case StatType.CriticalDamage:
                    GameManager.Instance.Player.critDamageUpgradeLevel = currentLevel;
                    break;
                case StatType.AutoAttack:
                    GameManager.Instance.Player.autoAttackSpeedUpgradeLevel = currentLevel;
                    break;
                case StatType.GoldBonus:
                    GameManager.Instance.Player.goldBonusUpgradeLevel = currentLevel;
                    break;
            }
        }
    }
    public void UpdateUpgradeUI()
    {
        if (GameManager.Instance != null && GameManager.Instance.Player != null)
    {
        Debug.Log("현재 골드: " + GameManager.Instance.Player.gold + " | 다음 레벨 비용: " + nextLevelCost);
    }
        levelText.text = upgradeData.upgradeName + " " + currentLevel.ToString();
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

        if (GameManager.Instance != null && GameManager.Instance.Player.gold >= nextLevelCost)
        {
            costText.color = affordableColor;
        }
        else
        {
            costText.color = insufficientColor;
        }
    }

    private void UpdateFinalStat()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("게임매니저 인스턴스를 찾을 수 없습니다!");
            return;
        }
        switch (statType)
        {
            case StatType.CriticalDamage:
                GameManager.Instance.Player.baseCritDamage = currentValue;
                break;
            case StatType.AutoAttack:
                if (autoAttack != null)
                {
                    autoAttack.UpdateAutoAttackSpeed(currentValue);
                }
                break;
            case StatType.GoldBonus:
                GameManager.Instance.Player.baseGoldBonus = currentValue;
                break;
        }
    }
}