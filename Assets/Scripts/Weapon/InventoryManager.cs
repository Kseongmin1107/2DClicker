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
        weaponLevels = new int[weaponList.Count];   // 각 무기 강화레벨 초기화
    }

    public WeaponStats GetCurrentWeapon()
    {
        return weaponList[currentWeaponIndex];      // 현재장착무기 스탯
    }

    public int GetCurrentWeaponLevel()
    {
        return weaponLevels[currentWeaponIndex];    // 현재장착무기 강화레벨
    }

    public void EnhanceCurrentWeapon()
    {
        weaponLevels[currentWeaponIndex]++;

        if (weaponLevels[currentWeaponIndex] > 5)
        {
            weaponLevels[currentWeaponIndex] = 0;
            currentWeaponIndex++;

            if (currentWeaponIndex >= weaponList.Count)
                currentWeaponIndex = weaponList.Count - 1;  // 마지막 무기면 더이상 넘어가지 않게
            Debug.Log("강화할 무기가 없다.");
        }
    }
}
