using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public ClickAttack clickAttack;

    private float autoAttackSpeed;
    private Coroutine autoAttackCoroutine;

    private void Awake()
    {
        if (clickAttack == null)
            clickAttack = GetComponent<ClickAttack>();
    }
    public void UpdateAutoAttackSpeed(float newSpeed)
    {
        autoAttackSpeed = newSpeed;

        if (autoAttackCoroutine != null)
        {
            StopCoroutine(autoAttackCoroutine);
        }
        if (autoAttackSpeed > 0)
        {
            autoAttackCoroutine = StartCoroutine(AutoAttackCoroutineLoop());
        }
    }

    private IEnumerator AutoAttackCoroutineLoop()
    {
        while (true)
        {
            clickAttack.Attack(true);
            Debug.Log("자동 공격중");
            yield return new WaitForSeconds(1f / autoAttackSpeed);
        }
    }
}