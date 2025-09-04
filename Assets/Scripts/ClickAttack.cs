using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAttack : MonoBehaviour
{
    [SerializeField] private ParticleSystem attackEffect;

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
            Debug.Log("클릭되었음");
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

        Weapon currentWeapon = WeaponManager.Instance.GetCurrentWeapon();
        float currentCriticalRate = 0f;
        float baseAttackPower = 0f;

        if (currentWeapon != null)
        {
            currentCriticalRate = currentWeapon.GetCriticalRate();
            baseAttackPower = currentWeapon.GetCurrentAttackPower();
        }

        bool isCritical = (Random.Range(0f, 1f) < currentCriticalRate); //예를들어서 치명타확률이 20%인데 0.15가 나오면 참으로 치명타가 발생하게됨


        float damage = baseAttackPower;

        if (isCritical)
        {
            damage *= GameManager.Instance.Player.baseCritDamage;
            Debug.Log("치명타 발생! 데미지: " + damage);
        }
        else
        {
            Debug.Log("일반 공격. 데미지: " + damage);
        }
        if (attackEffect != null)
        {
            attackEffect.transform.position = attackPosition;
            attackEffect.Play();
        }
        else
        {
            Debug.LogWarning("Attack Effect가 할당되지 않았습니다.");
        }
    }
}