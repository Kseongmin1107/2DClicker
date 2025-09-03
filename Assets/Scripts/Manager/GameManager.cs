using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerData player = new PlayerData();
    private PlayerGold playergold = new PlayerGold();

    public event Action<double> OnGoldChanged;
    public event Action<GameObject> OnSpendFailed;

    public PlayerData Player
    {
        get { return player; }
    }

    public PlayerGold playerGold
    {
        get { return playergold; }
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

        //테스트용골드 1000넣음
        playerGold.SetGold(1000);
    }

}
