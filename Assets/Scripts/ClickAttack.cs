using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ClickAttack : MonoBehaviour
{
    [SerializeField] private ParticleSystem attackEffect;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Player.Attack.performed += OnAttack;
        playerControls.Player.Enable();
    }

    private void OnDisable()
    {
        playerControls.Player.Attack.performed -= OnAttack;
        playerControls.Player.Disable();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        Attack();
    }

    public void Attack(bool isAutoAttack = false)
    {
        Enemy enemy = GameManager.Instance.StageManager.CurrentEnemy;
        if (enemy == null)
        {
            return;
        }
        float finalDamage = CalculateDamage();
        enemy.TakeDamage(finalDamage);

        PlayAttackEffect(enemy.transform.position);

        Debug.Log("���� �Ϸ�! " + (isAutoAttack ? "�ڵ� ����" : "Ŭ�� ����"));
    }


    private float CalculateDamage()
    {
        Weapon currentWeapon = WeaponManager.Instance.GetCurrentWeapon();
        float currentCriticalRate = 0f;
        float baseAttackPower = 0f;

        if (currentWeapon != null)
        {
            currentCriticalRate = currentWeapon.GetCriticalRate();
            baseAttackPower = currentWeapon.GetCurrentAttackPower();
        }

        bool isCritical = (Random.Range(0f, 1f) < currentCriticalRate);
        float damage = baseAttackPower;

        if (isCritical)
        {
            damage *= GameManager.Instance.baseCritDamage;
            Debug.Log("ġ��Ÿ �߻�! ������: " + damage);
        }
        else
        {
            Debug.Log("�Ϲ� ����. ������: " + damage);
        }

        return damage;
    }

    private void PlayAttackEffect(Vector3 position)
    {
        if (attackEffect != null)
        {
            attackEffect.transform.position = position;
            attackEffect.Play();
        }
        else
        {
            Debug.LogWarning("Attack Effect�� �Ҵ���� �ʾҽ��ϴ�.");
        }
    }
}