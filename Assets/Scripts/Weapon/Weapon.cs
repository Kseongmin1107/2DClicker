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

    [SerializeField] private Text attackText;   // ���� ����
    [SerializeField] private Text criticalText;

    public int level = 0;
    private int currentAttack;
    private float currentCritical;



    private void Start()
    {
        level = 0;
        UpdateEnhanceUI();
    }

    public void Enhance()
    {
            level++;
            UpdateEnhanceUI();

            Debug.Log($"+{level}��");
            Debug.Log($"���ݷ�:{currentAttack}, ġ��Ÿ:{currentCritical}");

        //int cost = GetEnhanceCost(level + 1);
        //if (gold < cost)
        //{
        //    Debug.Log("��尡 �����մϴ�");
        //    return;
        //}
        
        //gold -= cost;
    }

    //private int GetEnhanceCost(int nextLevel)
    //{
    //    return nextLevel * 200;     // ��ȭ��� ������ ����
    //}

    private void CalculateStats()
    {
        int weaponIndex = level / 6;
        int enhanceStep = level % 6;

        EnhanceLevel baseStat = statData.GetBaseStats(weaponIndex);

        if (baseStat != null)
        {
            currentAttack = baseStat.baseAttackPower + enhanceStep; // ��ȭ�ܰ躰 1������
            currentCritical = baseStat.baseCriticalRate + (0.05f * enhanceStep);    // 0.05�� ����
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
            levelTexts[i].SetActive(i == enhanceStep);   // �ؽ�Ʈ �ݺ��ǰ�

        if (weaponNameText != null && statData != null)
            weaponNameText.text = statData.GetWeaponName(level);    // ���� �̸�

        if (weaponMainNameText != null && statData != null)
            weaponMainNameText.text = statData.GetWeaponName(level);

        if (weaponImage != null && weaponSprites != null && weaponSprites.Length > spriteIndex)
        {
            weaponImage.sprite = weaponSprites[spriteIndex];       // ���� �̹���(��������Ʈ)
        }
        
        CalculateStats();

        //if (statData != null)
        //    //currentStat = statData.GetEnhanceLevel(level);     // ���� ���� ��������
        //else
        //    currentStat = new EnhanceLevel();


        if (attackText != null)
            attackText.text = "���ݷ� : " + currentAttack.ToString();
        if (criticalText != null)
            criticalText.text = "ġ��Ÿ : " + currentCritical.ToString("F2");      // �Ҽ������� �ٲ���

    }
}