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
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Ŭ���Ǿ���");
            return true;    // ��Ŭ��
        }
        else
            return false;
    }

    private void TryAttack()
    {
        // �Ͻ����� ���¸� �ߵ� X
        //if(PopupPause == true)
        //return;
        Attack();
    }

    public void Attack()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        clickPosition.z = 0;

        if (attackEffectPrefab != null)
        {
            Instantiate(attackEffectPrefab, clickPosition, Quaternion.identity);
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
