using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    private GameObject currentWeapon;

    public void EquipWeapon(GameObject weaponObject)
    {
        if (currentWeapon != null)
        {
            Debug.Log("장착 해제 후 장착하세요.");
            return;
        }

        currentWeapon = weaponObject;
        Debug.Log("무기 장착");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);  // 장착한 무기 삭제
            currentWeapon = null;
            Debug.Log("장착 해제");
        }
        else
        {
            Debug.Log("장착된 무기 없음");
        }
    }
}