using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public List<GameObject> weaponPrefab;

    private GameObject currentWeapon;

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon == weaponPrefab)
        {
            Debug.Log("이미 장착된 무기");
            return;
        }
        // 장착한무기와 장착하려는 무기가 다를때
        if (currentWeapon != null)
            UnequipWeapon();

        currentWeapon = Instantiate(weaponPrefab);
        Debug.Log("무기 장착");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);  // 장착한 무기 삭제
            Debug.Log("장착 해제");
            currentWeapon = null;
        }
        else
        {
            Debug.Log("장착된 무기 없음");
        }
    }
}
