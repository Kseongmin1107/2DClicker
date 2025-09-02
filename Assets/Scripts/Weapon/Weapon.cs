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
            Debug.Log("���̻� ��ȭ�� �� �����ϴ�.");
            return;
        }

        int cost = GetEnhanceCost(level + 1);
        if (gold < cost)
        {
            Debug.Log("��尡 �����մϴ�");
            return;
        }
        
        gold -= cost;

        level++;
        currentStat = statData.GetEnhanceLevel(level);

        UpdateEnhanceLevelTxt();

        Debug.Log($"+{level}��");
        Debug.Log($"���ݷ�:{currentStat.attackPower}, ġ��Ÿ:{currentStat.criticalRate}, ���ݼӵ�:{currentStat.attackSpeed}");
    }

    private int GetEnhanceCost(int nextLevel)
    {
        return nextLevel * 200;     // ��ȭ��� ������ ����
    }

    private void UpdateEnhanceLevelTxt()
    {
        for (int i = 0; i < enhanceLevelTxt.Length; i++)
        {
            if (enhanceLevelTxt[i] != null)
                enhanceLevelTxt[i].SetActive(i == level);   // ��ȭ�����ؽ�Ʈ ���������� �߰�
        }
    }
}