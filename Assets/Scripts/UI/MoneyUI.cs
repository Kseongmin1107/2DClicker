using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public TMP_Text haveGold;

    private void Start()
    {
        var gold = Player.Instance.Gold;
        if (gold != null)
        {
            gold.OnGoldChanged += HandleGoldChanged;
            HandleGoldChanged(gold.Gold);
        }
    }
    private void OnDisable()
    {
        var gold = Player.Instance.Gold;
        if(gold != null)
        {
            gold.OnGoldChanged -= HandleGoldChanged;
        }
    }

    void HandleGoldChanged(double changed)
    {
        if(haveGold != null)
        {
            haveGold.text = changed.ToString("0,0");
        }
    }
}
