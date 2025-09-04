using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "Game/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("Environment")]
    public Sprite map;
    public AudioClip bgm;

    [Header("Enemy")]
    public Enemy enemyPrefab;
    public int goldPerEnemy;
    public float enemyMaxHP;

    [Header("Open Condition")]
    public OpenCondition openCondition;

    [System.Serializable]
    public class OpenCondition
    {
        public int requiredWeaponIndex;
        public double goldCost;
    }
}
