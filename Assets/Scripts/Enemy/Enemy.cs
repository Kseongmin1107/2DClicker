using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Enemy : MonoBehaviour
{
    //public RectTransform front;
    [SerializeField] private Image back;

    public float Fullhealth = 100.0f;//���߿� �����Ӻ��� �ٸ� ü���� ���� �� �ֵ��� ����
    public float Nowhealth = 100.0f;//�������� ������ �� ���� �ִ� ü�°� ���� �����ϰ� �ٲ�
    public int score = 3;


    [SerializeField] private Animator animator;
    private static readonly int HashHit = Animator.StringToHash("Hit");
    private static readonly int HashDead = Animator.StringToHash("IsDead");

    private bool isDying;
    private double rewardGold;

    public event Action<Enemy> OnDied;

    // ���߰�: ���� �����ǰų� SetActive(true) �� �� �ڵ� �ʱ�ȭ
    private void OnEnable()
    {
        ResetForSpawn();   // �� ������ �� �׻� ȣ���
    }

    public void Update() // Ŭ�� �̺�Ʈ���� ��ä�� ������ �ӽ� �׽�Ʈ��

    {
        if (isDying)return;


    
    }
    public void TakeDamage(float damage)
    {
        if (isDying) return;

        Nowhealth = Nowhealth - damage;

        if (Nowhealth > 0)
        {
            float ratio = Mathf.Clamp01(Nowhealth / Mathf.Max(1f, Fullhealth));
            if (back) back.fillAmount = ratio;
            animator.SetTrigger(HashHit);
        }
        else
        {
            Nowhealth = 0f;
            IsDie();
        }
    }

    public void IsDie()
    {
        if (isDying)return;
        isDying = true;
        animator.SetBool(HashDead, true);
        GameManager.Instance.playerGold.AddGold(rewardGold);
        OnDied?.Invoke(this);
        gameObject.SetActive(false);
    }
    public void Init(float maxHP, double reward)
    {
        Fullhealth = maxHP;
        Nowhealth = Fullhealth;
        rewardGold = reward;
        if (back) back.fillAmount = 1f;
    }

    // ���߰�: HP��/�ִϸ�����/�÷��׸� ������ ������ �����·�
    private void ResetForSpawn()
    {
        isDying = false;              // �� ������ �κ�: ���������� false�� �ʱ�ȭ
        animator.ResetTrigger(HashHit);
        animator.SetBool(HashDead,false);
    }
}
