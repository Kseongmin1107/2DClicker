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

        UpdateCollection(weapon); // ���� ���� �� ���� ������Ʈ
    }

    public void UpdateCollection(Weapon currentWeapon)            // ���� �رݵ� �� ȣ��
    {
        if (currentWeapon == null || slotHideImage == null || weaponSprite == null)
            return;
        
        weapon = currentWeapon;

        int unlockedCount = (weapon.level / 6) + 1; // ���� ù��°�� �ٷ� �������ϴϱ�
        Debug.Log(unlockedCount);
        Debug.Log(weapon.level);

        for (int i = 0; i < slotHideImage.Length; i++)
        {
            if (i < unlockedCount && i < weaponSprite.Length)
            {
                slotHideImage[i].sprite = weaponSprite[i];
                slotHideImage[i].gameObject.SetActive(true);
                Debug.Log("�����̹�������");

                if (slotNameText != null && i < slotNameText.Length)
                    slotNameText[i].text = weapon.statData.GetWeaponName(i * 6);
                Debug.Log("�����ؽ�Ʈ����");
            }
        }
    }
}
