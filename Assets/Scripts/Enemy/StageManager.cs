using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public List<StageData> stages;

    public int currentStageIndex = 1;
    private StageData current;
    public Enemy CurrentEnemy;

    public Transform spawnPoint;

    public Image mapImage;
    public AudioSource bgmSource;

    Vector3 SpawnPos => spawnPoint.position;
    Quaternion spawnRot => spawnPoint.rotation;

    private void Start()
    {
        currentStageIndex = Mathf.Clamp(GameManager.Instance.Player.lastvisitedStage, 1, 30);
    }

    public void OpenOrGo(int index)
    {
        index = Mathf.Clamp(currentStageIndex, 1, 30);
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
        if (w.level < cond.requiredLevel)
        {
            return false;
        }

        return true;
    }

    private void ApplyMapAndBgm(StageData data)
    {
        if (mapImage)
        {
            mapImage.sprite = data.map;
        }
        if (bgmSource)
        {
            bgmSource.clip = data.bgm;
            if (bgmSource.clip) bgmSource.Play();
            else bgmSource.Stop();
        }
    }
    public void SpawnEnemy(StageData data)
    {
      
        CurrentEnemy = Instantiate(stages[currentStageIndex-1].enemyPrefab, SpawnPos, spawnRot);
        CurrentEnemy.Init(data.enemyMaxHP,data.goldPerEnemy);

        CurrentEnemy.OnDied += HandleEnemyDied;
        
    }

    private void HandleEnemyDied(Enemy ene)
    {
        ene.transform.SetPositionAndRotation(SpawnPos, spawnRot);
        ene.Init(current.enemyMaxHP, current.goldPerEnemy);
        ene.gameObject.SetActive(true);
    }



}
