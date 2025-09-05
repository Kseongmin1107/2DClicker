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
            currentWeapon = Instantiate(weaponPrefabs[index], enhanceSlot); // 현재무기 : 강화슬롯에 생성
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localScale = Vector3.one;

            Weapon weaponScript = currentWeapon.GetComponent<Weapon>();

            currentMainWeapon = Instantiate(weaponPrefabs[index], mainSlot); // 메인화면에 무기도 생성
            currentMainWeapon.transform.localPosition = Vector3.zero;
            currentMainWeapon.transform.localScale = Vector3.one;
        }
    }

    public void EnhanceCurrentWeapon()  //강화 버튼
    {
        Weapon currentWeapon = GetCurrentWeapon();
        if (currentWeapon != null)
            currentWeapon.Enhance();

        Weapon currentMainWeapon = GetCurrentMainWeapon();
        if (currentMainWeapon != null)
            currentMainWeapon.Enhance();
    }

    public Weapon GetCurrentWeapon()    // 현재 무기 반환
    {
        return currentWeapon != null ? currentWeapon.GetComponent<Weapon>() : null;
    }

    public Weapon GetCurrentMainWeapon()
    {
        return currentMainWeapon != null ? currentMainWeapon.GetComponent<Weapon>() : null;
    }
}
