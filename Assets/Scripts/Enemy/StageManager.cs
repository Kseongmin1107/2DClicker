using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public List<StageData> stages;

    public int currentStageIndex = 1;
    private StageData current;
    public Enemy CurrentEnemy;

    public Transform spawnPoint;

    public Image mapImage;
    public AudioSource bgmSource;
    public SpriteRenderer fieldBackGround;



    Vector3 SpawnPos => spawnPoint ? spawnPoint.position : Vector3.zero;
    Quaternion SpawnRot => spawnPoint ? spawnPoint.rotation : Quaternion.identity;

   

    private void Start()
    {
        currentStageIndex = Mathf.Clamp(GameManager.Instance.Player.lastvisitedStage, 1, 30);
        OpenOrGo(currentStageIndex);

    }

    public void OpenOrGo(int index)
    {
        index = Mathf.Clamp(index, 1, 30);
        if (IsUnlocked(index))
        {
            GoToStage(index);
        }
        else
        {
            if (TryOpenStage(index))
            {
                GoToStage(index);
            }
        }
    }

    private bool IsUnlocked(int index)
    {
        var list = GameManager.Instance.Player.unlockStages;
        return list.Contains(index);
    }

    private void GoToStage(int index)
    {
        currentStageIndex = index;
        current = stages[currentStageIndex-1];

        ApplyMapAndBgm(current);

        if (CurrentEnemy) Destroy(CurrentEnemy.gameObject);
        SpawnEnemy(current);

        GameManager.Instance.Player.lastvisitedStage = currentStageIndex;
        GameManager.Instance.Save();
    }

    private bool TryOpenStage(int index)
    {
        var data = stages[index-2];
        var cond = data.openCondition;

        if (IsRequireWeapon(cond))
        {
            if (GameManager.Instance.TrySpendGold(cond.goldCost))
            {
                Unlock(index);
                GameManager.Instance.Player.currentStage = index;
                return true;
            }
            return false;
        }

        return false;
    }

    private void Unlock(int index)
    {
        var list = GameManager.Instance.Player.unlockStages;
        list.Add(index);
        GameManager.Instance.Save();

    }

    public bool IsRequireWeapon(StageData.OpenCondition cond)
    {
        if (GameManager.Instance.Player.equippedWeaponLevel >= cond.requiredWeaponIndex)
        {
            return true;
        }

        return false;
    }

    private void ApplyMapAndBgm(StageData data)
    {
        if (data.map)
        {
            fieldBackGround.sprite = data.map;
        }
        if (data.bgm)
        {
            AudioManager.Instance.FadeTo(data.bgm, 1.0f);
        }
    }
    public void SpawnEnemy(StageData data)
    {
      
        CurrentEnemy = Instantiate(stages[currentStageIndex-1].enemyPrefab, SpawnPos, SpawnRot);
        CurrentEnemy.Init(data.enemyMaxHP,data.goldPerEnemy);

        CurrentEnemy.OnDied += HandleEnemyDied;
        
    }

    private void HandleEnemyDied(Enemy ene)
    {
        ene.transform.SetPositionAndRotation(SpawnPos, SpawnRot);
        ene.Init(current.enemyMaxHP, current.goldPerEnemy);
        ene.gameObject.SetActive(true);
    }



}
