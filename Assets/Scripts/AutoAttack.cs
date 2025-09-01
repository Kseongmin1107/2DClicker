using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public ClickAttack clickAttack;
    public int autoLevel = 1;   // 자동공격 레벨
    [SerializeField] private float interval = 1f; // 자동공격 간격

    private void Awake()
    {
        if (clickAttack == null)
            clickAttack = GetComponent<ClickAttack>();
    }

    public void Start()
    {
        StartCoroutine(AutoAttackCoroutine());
    }

    private IEnumerator AutoAttackCoroutine()
    {
        while (true)
        {
            clickAttack.Attack();
            Debug.Log("자동 공격중");
            yield return new WaitForSeconds(interval);
        }
    }
}
