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

        Weapon currentWeapon = WeaponManager.Instance.GetCurrentWeapon();
        float currentCriticalRate = 0f;
        float baseAttackPower = 0f;

        if (currentWeapon != null)
        {
            currentCriticalRate = currentWeapon.GetCriticalRate();
            baseAttackPower = currentWeapon.GetCurrentAttackPower();
        }

        bool isCritical = (Random.Range(0f, 1f) < currentCriticalRate); //������ ġ��ŸȮ���� 20%�ε� 0.15�� ������ ������ ġ��Ÿ�� �߻��ϰԵ�


        float damage = baseAttackPower;

        if (isCritical)
        {
            damage *= GameManager.Instance.Player.baseCritDamage;
            Debug.Log("ġ��Ÿ �߻�! ������: " + damage);
        }
        else
        {
            Debug.Log("�Ϲ� ����. ������: " + damage);
        }
        if (attackEffect != null)
        {
            attackEffect.transform.position = attackPosition;
            attackEffect.Play();
        }
        else
        {
            Debug.LogWarning("Attack Effect�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}