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
            Debug.Log("�̹� ������ ����");
            return;
        }
            UnequipWeapon();

        Debug.Log("���� ����");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);  // ������ ���� ����
            Debug.Log("���� ����");
            currentWeapon = null;
        }
        else
        {
            Debug.Log("������ ���� ����");
        }
    }
}
