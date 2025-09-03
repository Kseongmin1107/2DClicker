using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPopup;
    [SerializeField] private GameObject weaponEnhancePopup;

    public void OpenInventory()
    {
        inventoryPopup.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryPopup.SetActive(false);
    }

    public void OpenWeaponEnhance()
    {
        weaponEnhancePopup.SetActive(true);
    }

    public void CloseWeaponEnhance()
    {
        weaponEnhancePopup.SetActive(false);
    }
}
