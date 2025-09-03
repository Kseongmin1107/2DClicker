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
            Debug.Log("클릭되었음");
            return true;    // 좌클릭
        }
        else
            return false;
    }

    private void TryAttack()
    {
        // 일시정지 상태면 발동 X
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
            Debug.LogWarning("Attack Effect Prefab이 할당되지 않았습니다.");
        }
        // 공격 애니메이션
        // 데미지 처리
        // 점수 올라가게
    }
}
