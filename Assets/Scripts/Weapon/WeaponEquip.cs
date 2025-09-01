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
            Debug.Log("�̹� ������ ����");
            return;
        }
        // �����ѹ���� �����Ϸ��� ���Ⱑ �ٸ���
        if (currentWeapon != null)
            UnequipWeapon();

        currentWeapon = Instantiate(weaponPrefab);
        Debug.Log("���� ����");
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);  // ������ ���� ����
            Debug.Log("���� ����");
            currentWeapon = null;
        }
        else
        {
            Debug.Log("������ ���� ����");
        }
    }
}
