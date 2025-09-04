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

    [SerializeField] private Text attackText;   // �����ؽ�Ʈ ����
    [SerializeField] private Text criticalText;

    public int level = 0;
    private int currentAttack;
    private float currentCritical;

    public PlayerGold playerGold;   // ��� ����

    [SerializeField] private Text enhanceCostText;  // ��ȭ����ؽ�Ʈ


    private void Start()
    {
        
        level = GameManager.Instance.Player.equippedWeaponLevel;
       
        UpdateEnhanceUI();
    }

    public void Enhance()
    {
        int cost = GetEnhanceCost(level + 1);

        if (playerGold == null || !playerGold.TrySpendGold(cost))
        {
            Debug.Log("��尡 �����մϴ�");     // �˾�
            return;
        }

        level++;
        GameManager.Instance.Player.equippedWeaponLevel = level;
        UpdateEnhanceUI();
        GameManager.Instance.Save();

        Debug.Log($"+{level}��");
        Debug.Log($"���ݷ�:{currentAttack}, ġ��Ÿ:{currentCritical}");
        Debug.Log($"��ȭ��� {cost}");
    }

    private int GetEnhanceCost(int nextLevel)
    {
        if (statData == null) return 0;

        int weaponIndex = level / 6;
        int baseGold = statData.GetBaseGold(weaponIndex);

        return (baseGold / 20) * 15 * nextLevel;     // ���̽���� / 20 * 15
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