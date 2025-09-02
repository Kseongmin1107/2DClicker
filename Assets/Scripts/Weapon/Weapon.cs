using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats statData;
    //[SerializeField] private int gold = 1000;

    [SerializeField] private GameObject[] enhanceLevelTxt;

    [SerializeField] private Text attackText;   // 스탯 연결
    [SerializeField] private Text criticalText;
    [SerializeField] private Text attackSpeedText;

    private int level = 0;
    private EnhanceLevel currentStat;

    private void Start()
    {
        level = 0;
        currentStat = statData.GetEnhanceLevel(level);  // 스탯 맨처음으로 초기화
        UpdateEnhanceLevelTxt();
    }

    public void Enhance()
    {
        if (level < 5)
        {
            level++;
            currentStat = statData.GetEnhanceLevel(level);

            UpdateEnhanceLevelTxt();

            Debug.Log($"+{level}강");
            Debug.Log($"공격력:{currentStat.attackPower}, 치명타:{currentStat.criticalRate}, 공격속도:{currentStat.attackSpeed}");
        }
        else
        {
            WeaponManager.Instance.EquipNextWeapon();
            Destroy(gameObject);
        }
        //int cost = GetEnhanceCost(level + 1);
        //if (gold < cost)
        //{
        //    Debug.Log("골드가 부족합니다");
        //    return;
        //}
        
        //gold -= cost;


    }

    //private int GetEnhanceCost(int nextLevel)
    //{
    //    return nextLevel * 200;     // 강화비용 갈수록 증가
    //}

    private void UpdateEnhanceLevelTxt()
    {
        for (int i = 0; i < enhanceLevelTxt.Length; i++)
        {
            if (enhanceLevelTxt[i] != null)
                enhanceLevelTxt[i].SetActive(i == level);   // 강화레벨텍스트 각레벨마다 뜨게
        }

        // 텍스트 능력치 갱신
        currentStat = statData.GetEnhanceLevel(level);
        if (attackText != null)
            attackText.text = "공격력: " + currentStat.attackPower;
        if (criticalText != null)
            criticalText.text = "치명타: " + currentStat.criticalRate;
        if (attackSpeedText != null)
            attackSpeedText.text = "공속: " + currentStat.attackSpeed;
    }
}