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
        UpdateCollection(); // ���� ���� �� ���� ������Ʈ
    }

    public void UpdateCollection()            // ���� �رݵ� �� ȣ��
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

                if (slotNameText != null && i < slotNameText.Length)
                    slotNameText[i].text = weapon.statData.GetWeaponName(i * 6);
            }
        }
    }

    public void UnlockedCollection()
    {
        UpdateCollection();
    }
}
