using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollection : MonoBehaviour
{
    public GameObject[] slotHide;
    public Weapon weapon;

    public void UpdateCollection()
    {
        if (weapon == null)
            return;

        int spriteIndex = weapon.level / 6;

        for (int i = 0; i < slotHide.Length; i++)
        {
            slotHide[i].SetActive(i > spriteIndex);
        }
    }
}
