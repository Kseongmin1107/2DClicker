using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "New Stage")]

public class Stage : ScriptableObject
{
    public Sprite map;
    public Enemy enemyPrefab;

    //스테이지 이름, 적 이름(에너미 스크립트에서 불러옴,)
}
