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
        var data = stages[index-1];
        var cond = data.openCondition;

        if (IsRequireWeapon(cond))
        {
            //need +6enhance requireweapon popup
            return false;
        }

        if (cond.goldCost > 0)
        {
            if (!GameManager.Instance.TrySpendGold(cond.goldCost))
            {
                return false;
            }
        }

        Unlock(index);
        return true;
    }

    private void Unlock(int index)
    {
        var list = GameManager.Instance.Player.unlockStages;
        list.Add(index);
        GameManager.Instance.Save();

    }

    private bool IsRequireWeapon(StageData.OpenCondition cond)
    {
        var w = WeaponManager.Instance.GetCurrentWeapon();
        if (cond.requiredWeaponIndex >= 0 && GameManager.Instance.Player.equippedWeaponLevel != cond.requiredWeaponIndex)
        {
            return false;
        }

        return true;
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
