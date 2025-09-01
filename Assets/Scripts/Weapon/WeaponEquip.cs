using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    private Weapon currentWeapon;

    public void EquipWeapon()
    {
        if (currentWeapon != null)
            UnequipWeapon();

        Debug.Log("무기 장착");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);  // 장착한 무기 삭제
            currentWeapon = null;
            Debug.Log("장착 해제");
        }
        else
        {
            Debug.Log("장착된 무기 없음");
        }
    }
}
