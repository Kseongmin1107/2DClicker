using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponPrefabs;

    public Transform inventorySlot;     // ���Կ� ������������ ��Ÿ���� ��ġ����
    public Transform enhanceSlot;

    private int currentWeaponIndex = 0;
    private GameObject currentWeapon;

    public static WeaponManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        EquipWeapon(0);
    }

    public void EquipWeapon(int index)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);

        if (index < weaponPrefabs.Count)
        {
            currentWeapon = Instantiate(weaponPrefabs[index], enhanceSlot); // ���繫�� : ��ȭ���Կ� ����
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localScale = Vector3.one;

            GameObject copy = Instantiate(weaponPrefabs[index], inventorySlot); // ��ȭ���⸦ ���� �κ��丮���� �ٲ�� ����
            copy.transform.localPosition = Vector3.zero;
            copy.transform.localScale = Vector3.one;

            //Weapon weapon = copy.GetComponent<Weapon>();    // �κ��丮���� ��ũ��Ʈ �ʿ����
            //if (weapon != null)
            //    weapon.enabled = false;

            currentWeaponIndex = index;
        }
    }

    public void EquipNextWeapon()
    {
        int nextIndex = currentWeaponIndex + 1; // �������� ������ �ε��� 1�����ϱ�
        if (nextIndex < weaponPrefabs.Count)
        {
            EquipWeapon(nextIndex);
        }
        else
        {
            Debug.Log("�� �̻� ��ȭ�� �� �����ϴ�.");
        }
    }

    public void UnequipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
            currentWeapon = null;
        }
    }

    public void EnhanceCurrentWeapon()  //��ȭ ��ư
    {
        Weapon currentWeapon = GetCurrentWeapon();
        if (currentWeapon != null)
            currentWeapon.Enhance();
    }

    public Weapon GetCurrentWeapon()    // ���� ���� ��ȯ
    {
        return currentWeapon != null ? currentWeapon.GetComponent<Weapon>() : null; // ���� �ٽ� �غ���
    }
}
