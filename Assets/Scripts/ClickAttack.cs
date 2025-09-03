using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAttack : MonoBehaviour
{
    [SerializeField] private GameObject attackEffectPrefab;

    void Update()
    {
        if (IsClicked())
        {
            TryAttack();
        }
    }

    private bool IsClicked()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Ŭ���Ǿ���");
            return true;
        }

        return false;
    }

    private void TryAttack()
    {
        Attack();
    }

    public void Attack(bool isAutoAttack = false)
    {
        Vector3 attackPosition;

        if (isAutoAttack)
        {
            GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
            if (enemy != null)
            {
                attackPosition = enemy.transform.position;
            }
            else
            {
                return;
            }
        }
        else
        {
            attackPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            attackPosition.z = 0;
        }

        if (attackEffectPrefab != null)
        {
            Instantiate(attackEffectPrefab, attackPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Attack Effect Prefab�� �Ҵ���� �ʾҽ��ϴ�.");
        }
        // ���� �ִϸ��̼�
        // ������ ó��
        // ���� �ö󰡰�
    }
}