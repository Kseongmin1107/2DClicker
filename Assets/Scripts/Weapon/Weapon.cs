using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponStats statData;
    //[SerializeField] private int gold = 1000;

    [SerializeField] private Text weaponNameText;
    [SerializeField] private Text weaponMainNameText;
    [SerializeField] private GameObject[] levelTexts;
    public Image weaponImage;
    [SerializeField] private Sprite[] weaponSprites;

    [SerializeField] private Text attackText;   // 스탯텍스트 연결
    [SerializeField] private Text criticalText;

    public int level = 0;
    private int currentAttack;
    private float currentCritical;

    public PlayerGold playerGold;   // 골드 연결

    [SerializeField] private Text enhanceCostText;  // 강화비용텍스트


    private void Start()
    {
        level = 0;
        UpdateEnhanceUI();
    }

    public void Enhance()
    {
        int cost = GetEnhanceCost(level + 1);

        if (GameManager.Instance.playerGold == null || !GameManager.Instance.playerGold.TrySpendGold(cost))
        {
            Debug.Log("골드가 부족합니다");     // 팝업
            return;
        }

        level++;
        UpdateEnhanceUI();

        Debug.Log($"+{level}강");
        Debug.Log($"공격력:{currentAttack}, 치명타:{currentCritical}");
        Debug.Log($"강화비용 {cost}");
    }

    private int GetEnhanceCost(int nextLevel)
    {
        if (statData == null) return 0;

        int weaponIndex = level / 6;
        int baseGold = statData.GetBaseGold(weaponIndex);

        return (baseGold / 20) * 15 * nextLevel;     // 베이스골드 / 20 * 15
    }

    public void UpdateEnhanceCost()
    {
        if (enhanceCostText == null)
            return;

        int nextlevel = level + 1;
        int cost = GetEnhanceCost(nextlevel);

        enhanceCostText.text = $"{cost}";

        if (playerGold.Gold < cost)
            enhanceCostText.color = Color.red;
        else
            enhanceCostText.color = Color.yellow;
    }


    private void CalculateStats()
    {
        int weaponIndex = level / 6;
        int enhanceStep = level % 6;

        EnhanceLevel baseStat = statData.GetBaseStats(weaponIndex);

        if (baseStat != null)
        {
            currentAttack = baseStat.baseAttackPower + enhanceStep; // 강화단계별 1씩증가
            currentCritical = baseStat.baseCriticalRate + (0.05f * enhanceStep);    // 0.05씩 증가
        }
        else
        {
            currentAttack = 0;
            currentCritical = 0;
        }
    }

    public void UpdateEnhanceUI()
    {
        int enhanceStep = level % 6;
        int spriteIndex = level / 6;

        for (int i = 0; i < levelTexts.Length; i++)
            levelTexts[i].SetActive(i == enhanceStep);   // 텍스트 반복되게

        if (weaponNameText != null && statData != null)
            weaponNameText.text = statData.GetWeaponName(level);    // 무기 이름

        if (weaponMainNameText != null && statData != null)
            weaponMainNameText.text = statData.GetWeaponName(level);

        if (weaponImage != null && weaponSprites != null && weaponSprites.Length > spriteIndex)
        {
            weaponImage.sprite = weaponSprites[spriteIndex];       // 무기 이미지(스프라이트)
        }
        
        CalculateStats();

        //if (statData != null)
        //    //currentStat = statData.GetEnhanceLevel(level);     // 현재 스탯 가져오기
        //else
        //    currentStat = new EnhanceLevel();


        if (attackText != null)
            attackText.text = "공격력 : " + currentAttack.ToString();
        if (criticalText != null)
            criticalText.text = "치명타 : " + currentCritical.ToString("F2");      // 소수점으로 바꾸자

        UpdateEnhanceCost();

    }

    public float GetCriticalRate()
    {
        return currentCritical;
    }

    public int GetCurrentAttackPower()
    {
        return currentAttack;
    }
}