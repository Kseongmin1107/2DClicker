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
    public StageManager StageManager;

    public float baseCritDamage = 1.5f;

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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        Load();

        //Gold Test
        //if (playergold.Gold == 0)
        //{
        //    playerGold.SetGold(13000);
        //}

        //else
        //{
        //    playergold.SetGold(playerData.gold);
        //}
        playergold.OnGoldChanged += v => playerData.gold = v;
    }

    // Save and Load
    public void Save()
    {
        // ����ȭ
        var saveData = JsonUtility.ToJson(playerData);

        //����
        File.WriteAllText(Application.persistentDataPath + "/PlayerData.txt", saveData);

        Debug.Log(Application.persistentDataPath);
    }


    void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.txt"))
        {//�ҷ�����
            var loadData = File.ReadAllText(Application.persistentDataPath + "/PlayerData.txt");

            //������ȭ
            playerData = JsonUtility.FromJson<PlayerData>(loadData);
        }
        else
        {
            playerData = new PlayerData();
        }
    }


    // Game Reset
    public void ResetToDefaults()
    {
        if (File.Exists(Application.persistentDataPath + "/PlayerData.txt"))
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
