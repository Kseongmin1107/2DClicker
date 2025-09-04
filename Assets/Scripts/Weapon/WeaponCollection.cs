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

    public void UpdateCollection(int spriteIndex)      // ����������Ʈ
    {
        for (int i = 0; i < slotHide.Length; i++)
        {
            if (i <= spriteIndex)
            {
                slotHide[i].SetActive(false);        // ������ ����
                slotImage[i].sprite = weaponSprite[i]; // �̹��� ǥ��
            }
            else
            {
                slotHide[i].SetActive(true);
            }
        }
    }
}
