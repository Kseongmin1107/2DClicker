using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour
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
            return true;    // ��Ŭ��
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

    private void Attack()
    {
        // ���� �ִϸ��̼�
        // ������ ó��
        // ���� �ö󰡰�
    }
}
