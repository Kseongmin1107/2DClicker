using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCollection : MonoBehaviour
{
    [SerializeField] private Image[] slotImage;
    [SerializeField] private GameObject[] slotHide;
    [SerializeField] private Sprite[] weaponSprite;

    public Weapon weapon;

    public void UpdateCollection(int spriteIndex)      // 도감업데이트
    {
        for (int i = 0; i < slotHide.Length; i++)
        {
            if (i <= spriteIndex)
            {
                slotHide[i].SetActive(false);        // 가림막 제거
                slotImage[i].sprite = weaponSprite[i]; // 이미지 표시
            }
            else
            {
                slotHide[i].SetActive(true);
            }
        }
    }
}
