using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableObject : MonoBehaviour
{
    public RectTransform front;

    public float Fullhealth = 100.0f;
    public float Nowhealth = 100.0f;

    public float Hitdamage = 5; //이 값은 나중에 공격력을 받아오는걸로 교체


    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsDamaged();

        }
    }
    public void IsDamaged()
    {
        
        Nowhealth = Nowhealth - Hitdamage;
        front.localScale = new Vector3(Nowhealth / Fullhealth, 1.0f);

    }
    public void IsDie()
    {

    }


}
