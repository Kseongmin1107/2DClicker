using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCollection : MonoBehaviour
{
    [SerializeField] private Image[] slotHideImage;
    [SerializeField] private Sprite[] weaponSprite;
    [SerializeField] private Text[] slotNameText;

    public Weapon weapon;

    private void Start()
    {
        if (weapon == null)
            weapon = WeaponManager.Instance.GetCurrentWeapon();

        UpdateCollection(weapon); // 게임 시작 시 도감 업데이트
    }

    public void UpdateCollection(Weapon currentWeapon)            // 무기 해금될 때 호출
    {
        if (currentWeapon == null || slotHideImage == null || weaponSprite == null)
            return;
        
        weapon = currentWeapon;

        int unlockedCount = (weapon.level / 6) + 1; // 도감 첫번째는 바로 열려야하니까
        Debug.Log(unlockedCount);
        Debug.Log(weapon.level);

        for (int i = 0; i < slotHideImage.Length; i++)
        {
            if (i < unlockedCount && i < weaponSprite.Length)
            {
                slotHideImage[i].sprite = weaponSprite[i];
                slotHideImage[i].gameObject.SetActive(true);
                Debug.Log("슬롯이미지오픈");

                if (slotNameText != null && i < slotNameText.Length)
                    slotNameText[i].text = weapon.statData.GetWeaponName(i * 6);
                Debug.Log("슬롯텍스트오픈");
            }
        }
    }
}
