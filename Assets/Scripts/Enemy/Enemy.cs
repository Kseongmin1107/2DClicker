using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //public RectTransform front;
    [SerializeField] private Image back;

    public float Fullhealth = 100.0f;//나중에 슬라임별로 다른 체력을 가질 수 있도록 만듬
    public float Nowhealth = 100.0f;//슬라임을 생성할 때 마다 최대 체력과 같게 설정하게 바꿈
    public float Hitdamage = 5; //이 값은 나중에 공격력을 받아오는걸로 교체
    public int score = 3;


    [SerializeField] private Animator animator;
    private static readonly int HashHit = Animator.StringToHash("Hit");
    private static readonly int HashDie = Animator.StringToHash("Die");

    private bool isDying;

    private void Start()
    {
        if (back) back.fillAmount = Mathf.Clamp01(Nowhealth / Mathf.Max(1f, Fullhealth));

    }

    public void Update() // 클릭 이벤트에서 대채함 지금은 임시 테스트용

    {
        if (isDying)return;


        if (Input.GetMouseButtonDown(0))
        {

            IsDamaged();

        }
    }
    public void IsDamaged()
    {
        if (isDying) return;
        Nowhealth = Nowhealth - Hitdamage;
        if (Nowhealth > 0)
        {

            //front.localScale = new Vector3(Nowhealth / Fullhealth, 1.0f);
            float ratio = Mathf.Clamp01(Nowhealth / Mathf.Max(1f, Fullhealth));
            if (back) back.fillAmount = ratio;              // ← fillAmount로 반영
            animator.SetTrigger(HashHit);
        }
        else
        {
            //front.localScale = new Vector3(0.0f, 1.0f);//필 어마운트로 교체
            if (back) back.fillAmount = 0f;                 // ← 0으로 고정

            IsDie();
        }
        
    }
    public void IsDie()
    {
        if (isDying)return;

        isDying = true;
        animator.SetTrigger(HashDie);
        Debug.Log("으앙주금");
    }
}
