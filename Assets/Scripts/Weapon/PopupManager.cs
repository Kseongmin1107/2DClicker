using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private GameObject CollectionPopup;
    [SerializeField] private GameObject weaponEnhancePopup;

    public void OpenCollection()
    {
        CollectionPopup.SetActive(true);
    }

    public void CloseCollection()
    {
        CollectionPopup.SetActive(false);
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
