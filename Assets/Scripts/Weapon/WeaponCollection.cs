using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCollection : MonoBehaviour
{
    [SerializeField] private Image[] slotHideImage;
    [SerializeField] private Sprite[] weaponSprite;

    public Weapon weapon;

    private void Start()
    {
        UpdateCollection(); // 게임 시작 시 도감 업데이트
    }

    public void UpdateCollection()            // 무기 해금될 때 호출
    {
        if (weapon == null || slotHideImage == null || weaponSprite == null)
            return;

        int unlockedCount = (weapon.level / 6) + 1;

        for (int i = 0; i < slotHideImage.Length; i++)
        {
            if (i < unlockedCount && i < weaponSprite.Length)
            {
                slotHideImage[i].sprite = weaponSprite[i];
                slotHideImage[i].gameObject.SetActive(true);
            }
        }
    }

    public void UnlockedCollection()
    {
        UpdateCollection();
    }
}
