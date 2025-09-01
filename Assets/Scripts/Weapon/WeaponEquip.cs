using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    private GameObject currentWeapon;

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (currentWeapon != null)
        {
            Debug.Log("���� ���� �� �����ϼ���.");
            return;
        }

        currentWeapon = Instantiate(weaponPrefab);
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
