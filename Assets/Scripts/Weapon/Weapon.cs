using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public WeaponStats statData;
    //[SerializeField] private int gold = 1000;

    [SerializeField] private Text weaponNameText;
    [SerializeField] private GameObject[] levelTexts;
    public Image weaponImage;
    [SerializeField] private Sprite[] weaponSprites;

    [SerializeField] private Text attackText;   // ���� ����
    [SerializeField] private Text criticalText;

    public int level = 0;
    private EnhanceLevel currentStat;

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
            Debug.Log($"���ݷ�:{currentStat.attackPower}, ġ��Ÿ:{currentStat.criticalRate}");

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

    public void UpdateEnhanceUI()
    {
        int localLevel = level % 6;
        int spriteIndex = level / 6;

        for (int i = 0; i < levelTexts.Length; i++)
            levelTexts[i].SetActive(i == localLevel);   // �ؽ�Ʈ �ݺ��ǰ�

        if (weaponNameText != null && statData != null)
            weaponNameText.text = statData.GetWeaponName(level);    // ���� �̸�

        if (weaponImage != null && weaponSprites != null && weaponSprites.Length > spriteIndex)
        {
            weaponImage.sprite = weaponSprites[spriteIndex];       // ���� �̹���(��������Ʈ)
        }

        if (statData != null)
            currentStat = statData.GetEnhanceLevel(level);     // ���� ���� ��������
        else
            currentStat = new EnhanceLevel();


        if (attackText != null)
            attackText.text = "���ݷ� : " + currentStat.attackPower.ToString();
        if (criticalText != null)
            criticalText.text = "ġ��Ÿ : " + currentStat.criticalRate.ToString("F2");      // �Ҽ������� �ٲ���

    }
}