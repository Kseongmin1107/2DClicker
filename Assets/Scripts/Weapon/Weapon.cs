using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponStats statData;
    //[SerializeField] private int gold = 1000;

    [SerializeField] private GameObject[] enhanceLevelTxt;

    [SerializeField] private Text attackText;   // ���� ����
    [SerializeField] private Text criticalText;
    [SerializeField] private Text attackSpeedText;

    private int level = 0;
    private EnhanceLevel currentStat;

    private void Start()
    {
        level = 0;
        currentStat = statData.GetEnhanceLevel(level);  // ���� ��ó������ �ʱ�ȭ
        UpdateEnhanceLevelTxt();
    }

    public void Enhance()
    {
        if (level < 5)
        {
            level++;
            currentStat = statData.GetEnhanceLevel(level);

            UpdateEnhanceLevelTxt();

            Debug.Log($"+{level}��");
            Debug.Log($"���ݷ�:{currentStat.attackPower}, ġ��Ÿ:{currentStat.criticalRate}, ���ݼӵ�:{currentStat.attackSpeed}");
        }
        else
        {
            WeaponManager.Instance.EquipNextWeapon();
            Destroy(gameObject);
        }
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

    private void UpdateEnhanceLevelTxt()
    {
        for (int i = 0; i < enhanceLevelTxt.Length; i++)
        {
            if (enhanceLevelTxt[i] != null)
                enhanceLevelTxt[i].SetActive(i == level);   // ��ȭ�����ؽ�Ʈ ���������� �߰�
        }

        // �ؽ�Ʈ �ɷ�ġ ����
        currentStat = statData.GetEnhanceLevel(level);
        if (attackText != null)
            attackText.text = "���ݷ�: " + currentStat.attackPower;
        if (criticalText != null)
            criticalText.text = "ġ��Ÿ: " + currentStat.criticalRate;
        if (attackSpeedText != null)
            attackSpeedText.text = "����: " + currentStat.attackSpeed;
    }
}