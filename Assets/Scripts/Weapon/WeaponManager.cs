using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<GameObject> weaponPrefabs;
    public List<WeaponStats> weaponStats;

    public Transform inventorySlot;     // 슬롯에 무기프리팹이 나타나게 위치설정
    public Transform enhanceSlot;

    private int currentWeaponIndex = 0;
    private GameObject currentWeapon;
    private GameObject inventoryCopy;

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
            currentWeapon = Instantiate(weaponPrefabs[index], enhanceSlot); // 현재무기 : 강화슬롯에 생성
            currentWeapon.transform.localPosition = Vector3.zero;
            currentWeapon.transform.localScale = Vector3.one;

            Weapon weaponScript = currentWeapon.GetComponent<Weapon>();

            GameObject copy = Instantiate(currentWeapon, inventorySlot); // 강화무기슬롯을 따라서 인벤토리에도 바뀌게 생성
            copy.transform.localPosition = Vector3.zero;
            copy.transform.localScale = Vector3.one;
        }
    }

    public void EquipNextWeapon()
    {
        int nextIndex = currentWeaponIndex + 1; // 다음무기 프리팹 인덱스 1씩더하기
        if (nextIndex < weaponPrefabs.Count)
        {
            EquipWeapon(nextIndex);
        }
        else
        {
            Debug.Log("더 이상 강화할 수 없습니다.");
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

    public void EnhanceCurrentWeapon()  //강화 버튼
    {
        Weapon currentWeapon = GetCurrentWeapon();
        if (currentWeapon != null)
            currentWeapon.Enhance();
    }

    public Weapon GetCurrentWeapon()    // 현재 무기 반환
    {
        return currentWeapon != null ? currentWeapon.GetComponent<Weapon>() : null; // 여기 다시 해보기
    }
}
