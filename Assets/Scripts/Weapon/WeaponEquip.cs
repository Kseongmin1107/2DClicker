using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public List<GameObject> weaponPrefab;

    private List<Weapon> currentWeapon = new List<Weapon>();

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon.Contains(weaponPrefab))
        {
            Debug.Log("이미 장착된 무기");
            return;
        }
            UnequipWeapon();

        Debug.Log("무기 장착");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);  // 장착한 무기 삭제
            Debug.Log("장착 해제");
            currentWeapon = null;
        }
        else
        {
            Debug.Log("장착된 무기 없음");
        }
    }
}
