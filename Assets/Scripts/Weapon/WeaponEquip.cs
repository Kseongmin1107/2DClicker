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

        Debug.Log("���� ����");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);  // ������ ���� ����
            currentWeapon = null;
            Debug.Log("���� ����");
        }
        else
        {
            Debug.Log("������ ���� ����");
        }
    }
}
