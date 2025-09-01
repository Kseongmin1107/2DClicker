using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAttack : MonoBehaviour
{
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
        // ���� �ִϸ��̼�
        // ������ ó��
        // ���� �ö󰡰�
    }
}
