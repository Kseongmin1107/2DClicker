using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Stage[] stages;

    public int currentWave = 0;
    public int currentStage = 0;

    public int maxWave = 10;

    public Enemy CurrentEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (currentWave == 0)
        {
            CurrentEnemy = Instantiate(stages[currentStage].enemyPrefab);
        }
        else
        {
            CurrentEnemy.gameObject.SetActive(false);
            CurrentEnemy.Fullhealth *= 2;
            CurrentEnemy.Init();
            CurrentEnemy.gameObject.SetActive(true);
        }
    }

    public void CheckWave()
    {   
        if (currentWave < maxWave)
        {
            currentWave++;
            SpawnEnemy() ;
        }
        else
        {
            Destroy(CurrentEnemy.gameObject);
            currentStage++;
            currentWave = 0;
            SpawnEnemy();
        }
    }
}
