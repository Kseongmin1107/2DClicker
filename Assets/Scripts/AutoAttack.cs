using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public ClickAttack clickAttack;
    public int autoLevel = 1;   // �ڵ����� ����
    [SerializeField] private float interval = 1f; // �ڵ����� ����

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
            Debug.Log("�ڵ� ������");
            yield return new WaitForSeconds(interval);
        }
    }
}
