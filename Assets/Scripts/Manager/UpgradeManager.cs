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
        // ��峪 ����� ������� Ȯ���ϴ� ���� �߰�
        // if (GameManager.Instance.gold >= nextLevelCost)
        // {
        //     GameManager.Instance.SpendGold(nextLevelCost); // ��� �Ҹ�
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
        //�ϼ��� �ּ�����
        //if (GameManager.Instance != null && GameManager.Instance.gold >= nextLevelCost)
        //{
        //    costText.color = affordableColor; // ����ϸ� ������
        //}
        //else
        //{
        //    costText.color = insufficientColor; // �����ϸ� ������
        //}
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