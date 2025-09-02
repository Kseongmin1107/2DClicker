using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] public WeaponStats statData;
    //[SerializeField] private int gold = 1000;

    [SerializeField] private GameObject[] enhanceLevelTxt;

    [SerializeField] private Text attackText;   // ���� ����
    [SerializeField] private Text criticalText;
    [SerializeField] private Text attackSpeedText;

    public int level = 0;
    private EnhanceLevel currentStat;

    private void Start()
    {
        level = 0;
        UpdateEnhanceText();
        UpdateEnhanceLevelTxt();
    }

    public void Enhance()
    {
        if (level < 5)
        {
            level++;
            UpdateEnhanceText();
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

    public void UpdateEnhanceLevelTxt()
    {
        for (int i = 0; i < enhanceLevelTxt.Length; i++)
        {
            if (enhanceLevelTxt[i] != null)
                enhanceLevelTxt[i].SetActive(i == level);   // ��ȭ�����ؽ�Ʈ ���������� �߰�
        }
    }

    public void UpdateEnhanceText()
    {
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