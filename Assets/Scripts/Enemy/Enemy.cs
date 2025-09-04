using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Enemy : MonoBehaviour
{
    //public RectTransform front;
    [SerializeField] private Image back;

    [SerializeField] private TextMeshProUGUI damageText;
    private float damageLifeTime = 0.5f;

    private Coroutine HpbarCo;
    private Coroutine damageTextCo;

    public float Fullhealth = 100.0f;
    public float Nowhealth = 100.0f;
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

    public void Update()

    {
        if (isDying)return;


    
    }
    public void TakeDamage(float damage)
    {
        if (isDying) return;

        Nowhealth = Nowhealth - damage;
        ShowDamageText(damage);

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

    IEnumerator SetHpbar(float ratio)
    {
        while (back.fillAmount <= ratio)
        {
            back.fillAmount -= 0.1f * Time.deltaTime;
            yield return null;
        }
        back.fillAmount = ratio;

    }

    private void ShowDamageText(float damage)//피해량 받아오기 필요
    {
        damageText.text = damage.ToString();
        damageText.gameObject.SetActive(true);

        StopCoroutine(damageTextCo);
        damageTextCo = StartCoroutine(HideDamageText());
    }

    private IEnumerator HideDamageText()
    {
        yield return new WaitForSeconds(damageLifeTime);
        damageText.gameObject.SetActive(false);
    }



    public void IsDie()
    {
        if (isDying)return;
        isDying = true;
        animator.SetBool(HashDead, true);
        GameManager.Instance.playerGold.AddGold(rewardGold);
        gameObject.SetActive(false);
        OnDied?.Invoke(this);
    }
    public void Init(float maxHP, double reward)
    {
        Fullhealth = maxHP;
        Nowhealth = Fullhealth;
        rewardGold = reward;
        if (back) back.fillAmount = 1f;
    }


    private void ResetForSpawn()
    {
        isDying = false; 
        animator.ResetTrigger(HashHit);
        animator.SetBool(HashDead,false);
    }
}
