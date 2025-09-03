using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponPrefabs;
    public List<WeaponStats> weaponStats;

    public Transform inventorySlot;     // ���Կ� ������������ ��Ÿ���� ��ġ����
    public Transform enhanceSlot;

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

            Weapon weaponScript = currentWeapon.GetComponent<Weapon>();
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
