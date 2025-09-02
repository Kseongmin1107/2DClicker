using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<WeaponStats> weaponList;
    public int currentWeaponIndex = 0;
    public int[] weaponLevels;

    private void Awake()
    {
        Instance = this;
        weaponLevels = new int[weaponList.Count];   // �� ���� ��ȭ���� �ʱ�ȭ
    }

    public WeaponStats GetCurrentWeapon()
    {
        return weaponList[currentWeaponIndex];      // ������������ ����
    }

    public int GetCurrentWeaponLevel()
    {
        return weaponLevels[currentWeaponIndex];    // ������������ ��ȭ����
    }

    public void EnhanceCurrentWeapon()
    {
        weaponLevels[currentWeaponIndex]++;

        if (weaponLevels[currentWeaponIndex] > 5)
        {
            weaponLevels[currentWeaponIndex] = 0;
            currentWeaponIndex++;

            if (currentWeaponIndex >= weaponList.Count)
                currentWeaponIndex = weaponList.Count - 1;  // ������ ����� ���̻� �Ѿ�� �ʰ�
            Debug.Log("��ȭ�� ���Ⱑ ����.");
        }
    }
}
