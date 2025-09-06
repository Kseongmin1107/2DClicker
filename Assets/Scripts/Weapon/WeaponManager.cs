using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponPrefabs;
    public List<WeaponStats> weaponStats;

    public Transform enhanceSlot;
    public Transform mainSlot;

    private GameObject currentWeapon;
    private GameObject currentMainWeapon;

    public WeaponCollection weaponCollection;

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
        if (currentMainWeapon != null)
            Destroy(currentMainWeapon);

        if (index < weaponPrefabs.Count)
        {
            currentWeapon = Instantiate(weaponPrefabs[index], enhanceSlot); // ���繫�� : ��ȭ���Կ� ����
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localScale = Vector3.one;

            Weapon weaponScript = currentWeapon.GetComponent<Weapon>();

            currentMainWeapon = Instantiate(weaponPrefabs[index], mainSlot); // ����ȭ�鿡 ���⵵ ����
            currentMainWeapon.transform.localPosition = Vector3.zero;
            currentMainWeapon.transform.localScale = Vector3.one;
        }
    }

    public void EnhanceCurrentWeapon()  //��ȭ ��ư
    {
        Weapon currentWeapon = GetCurrentWeapon();
        if (currentWeapon != null)
            currentWeapon.Enhance();

        Weapon currentMainWeapon = GetCurrentMainWeapon();
        if (currentMainWeapon != null)
            currentMainWeapon.Enhance();
    }

    public Weapon GetCurrentWeapon()    // ���� ���� ��ȯ
    {
        return currentWeapon != null ? currentWeapon.GetComponent<Weapon>() : null;
    }

    public Weapon GetCurrentMainWeapon()
    {
        return currentMainWeapon != null ? currentMainWeapon.GetComponent<Weapon>() : null;
    }
}
