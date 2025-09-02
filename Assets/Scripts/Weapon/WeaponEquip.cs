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
            Debug.Log("���� ���� �� �����ϼ���.");
            return;
        }

        currentWeapon = weaponObject;
        Debug.Log("���� ����");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);  // ������ ���� ����
            currentWeapon = null;
            Debug.Log("���� ����");
        }
        else
        {
            Debug.Log("������ ���� ����");
        }
    }
}