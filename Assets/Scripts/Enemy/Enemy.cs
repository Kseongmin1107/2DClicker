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
    public float Hitdamage = 5; //�� ���� ���߿� ���ݷ��� �޾ƿ��°ɷ� ��ü
    public int score = 3;


    [SerializeField] private Animator animator;
    private static readonly int HashHit = Animator.StringToHash("Hit");
    private static readonly int HashDead = Animator.StringToHash("IsDead");

    private bool isDying;
    private double rewardGold;

    public event Action<Enemy> OnDied;


    private void OnEnable()
    {
        ResetForSpawn();  
    }

    public void IsDamaged()
    {
        if (isDying) return;
        Nowhealth = Nowhealth - Hitdamage;
        if (Nowhealth > 0)
        {
            animator.SetTrigger(HashHit);
        }
        else
        {
            Nowhealth = 0f;
            IsDie();
        }
        float ratio = Mathf.Clamp01(Nowhealth / Mathf.Max(1f, Fullhealth));
        if (HpbarCo != null)
        {
            StopCoroutine(HpbarCo);
        }
        HpbarCo = StartCoroutine(SetHpbar(ratio));
    }

    private Coroutine HpbarCo;
    IEnumerator SetHpbar(float ratio)
    {
        while (back.fillAmount <= ratio)
        {
            back.fillAmount -= 0.1f*Time.deltaTime;
            yield return null;
        }
        back.fillAmount = ratio;
    
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
