using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponStats statData;
    //[SerializeField] private int gold = 1000;

    [SerializeField] private Text levelText;
    [SerializeField] private Image levelImage;  // 이미지
    [SerializeField] private Sprite[] levelSprites;

    [SerializeField] private Text attackText;   // 스탯 연결
    [SerializeField] private Text criticalText;
    [SerializeField] private Text attackSpeedText;

    public int level = 0;
    private EnhanceLevel currentStat;

    private void StartData(WeaponStats data)        // 스타트말고 메서드 사용해서 원할때 쓸수 있게
    {
        statData = data;
        level = 0;
        UpdateEnhanceUI();
    }

    public void Enhance()
    {
        int localLevel = level % 6;     // 0~ 5강 = 6개 반복되게 하기

        if (localLevel < 5)
        {
            level++;
            UpdateEnhanceUI();

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

    public void UpdateEnhanceUI()
    {
        int localLevel = level % 6;

        if (levelText != null)
            levelText.text = "+" + localLevel;  // +0 ~ +5
        
        if (levelImage != null && levelSprites != null && levelSprites.Length > localLevel)
            levelImage.sprite = levelSprites[localLevel];       // 이미지(스프라이트)

        if (statData != null)
            currentStat = statData.GetEnhanceLevel(localLevel);     // 현재 스탯 가져오기
        else
            currentStat = new EnhanceLevel();

        if (attackText != null)
            attackText.text = "공격력 : " + currentStat.attackPower.ToString();
        if (criticalText != null)
            criticalText.text = "치명타 : " + currentStat.criticalRate.ToString("F2");      // 소수점으로 바꾸자
        if (attackSpeedText != null)
            attackSpeedText.text = "공격속도 : " + currentStat.attackSpeed.ToString("F2");

    }
}