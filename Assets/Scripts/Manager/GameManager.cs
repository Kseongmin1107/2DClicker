using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerData player = new PlayerData();

    public event Action<double> OnGoldChanged;
    public event Action<GameObject> OnSpendFailed;

    public GameObject GoldWarningPopup;

    public PlayerData Player
    {
        get { return player; }
    }


    private void Awake()
    {
        if( Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // 재화 관리
    public void AddGold(float amount)
    {
        amount = Mathf.Max(0, amount);
        player.gold += amount;
        OnGoldChanged?.Invoke(player.gold);
    }

    public bool TrySpendGold(double cost)
    {
        if (player.gold < cost)
        {
            OnSpendFailed?.Invoke(GoldWarningPopup);
            return false;
        }
        player.gold -= cost;
        OnGoldChanged?.Invoke(player.gold);
        return true;
    }
}
