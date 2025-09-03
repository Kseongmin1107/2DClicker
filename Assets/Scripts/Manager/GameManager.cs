using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private PlayerData playerData = new PlayerData();
    private PlayerGold playergold = new PlayerGold();

    public event Action<double> OnGoldChanged;

    public PlayerData Player
    {
        get { return playerData; }
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

        Load();

        //테스트용골드 1000넣음
        if (playergold.Gold == -1)
        {
            playerGold.SetGold(1000);
        }

        else
        {
            playergold.SetGold(playerData.gold);
        }
        playergold.OnGoldChanged += v => playerData.gold = v;
    }


    // Save and Load
    void Save()
    {
        // 직렬화
        var saveData = JsonUtility.ToJson(playerData);

        //저장
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.txt", saveData);

        Debug.Log(Application.persistentDataPath);
    }


    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.txt"))
        {//불러오기
            var loadData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.txt");

            //역직렬화
            playerData = JsonUtility.FromJson<PlayerData>(loadData);
        }
        else
        {
            playerData  = new PlayerData();
        }
    }


    // Game Reset
    public void ResetToDefaults()
    {
        if(File.Exists(Application.persistentDataPath + "/PlayerData.txt"))
        {
            File.Delete(Application.persistentDataPath + "/PlayerData.txt");
        }

        playerData = new PlayerData();

        playergold.SetGold(playerData.gold);

        Save();
    }

    public bool TrySpendGold(double cost)
    {
        bool success = playerGold.TrySpendGold(cost);
        if (!success)
        {
            UIManager.Instance.warningPopup.Show();
        }
        return success;
    }


}
