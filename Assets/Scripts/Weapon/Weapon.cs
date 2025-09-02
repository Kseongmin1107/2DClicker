using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats statData;
    [SerializeField] private int gold = 1000;

    [SerializeField] private GameObject[] enhanceLevelTxt;

    private int level;
    private EnhanceLevel currentStat;

    private void Start()
    {
        UpdateEnhanceLevelTxt();
    }

    public void Enhance()
    {
        if (level >= 5)
        {
            Debug.Log("더이상 강화할 수 없습니다.");
            return;
        }

        int cost = GetEnhanceCost(level + 1);
        if (gold < cost)
        {
            Debug.Log("골드가 부족합니다");
            return;
        }
        
        gold -= cost;

        level++;
        currentStat = statData.GetEnhanceLevel(level);

        UpdateEnhanceLevelTxt();

        Debug.Log($"+{level}강");
        Debug.Log($"공격력:{currentStat.attackPower}, 치명타:{currentStat.criticalRate}, 공격속도:{currentStat.attackSpeed}");
    }

    private int GetEnhanceCost(int nextLevel)
    {
        return nextLevel * 200;     // 강화비용 갈수록 증가
    }

    private void UpdateEnhanceLevelTxt()
    {
        for (int i = 0; i < enhanceLevelTxt.Length; i++)
        {
            if (enhanceLevelTxt[i] != null)
                enhanceLevelTxt[i].SetActive(i == level);   // 강화레벨텍스트 각레벨마다 뜨게
        }
    }
}