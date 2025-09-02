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
        UpdateHpUI();
    }

    // ★추가: 새로 스폰되거나 SetActive(true) 될 때 자동 초기화
    private void OnEnable()
    {
        ResetForSpawn();   // ← 리스폰 시 항상 호출됨
    }

    public void Update() // 클릭 이벤트에서 대채함 지금은 임시 테스트용

    {
        if (isDying)return;


        if (Input.GetMouseButtonDown(0))
        {

            IsDamaged();
            Debug.Log("아야");

        }
    }
    public void IsDamaged()
    {
        if (isDying) return;
        Nowhealth = Nowhealth - Hitdamage;
        if (Nowhealth > 0)
        {

            //front.localScale = new Vector3(Nowhealth / Fullhealth, 1.0f);
            //float ratio = Mathf.Clamp01(Nowhealth / Mathf.Max(1f, Fullhealth));
            //if (back) back.fillAmount = ratio;              // ← fillAmount로 반영
            UpdateHpUI();
            animator.SetTrigger(HashHit);
        }
        else
        {
            //front.localScale = new Vector3(0.0f, 1.0f);//필 어마운트로 교체
            Nowhealth = 0f;
            UpdateHpUI();                 // ← 0으로 고정

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

    // ★추가: HP바/애니메이터/플래그를 리스폰 시점에 원상태로
    private void ResetForSpawn()
    {
        isDying = false;              // ← 질문한 부분: 리스폰마다 false로 초기화
        Nowhealth = Fullhealth;       // 풀 체력

        // HP UI
        UpdateHpUI();

        // 애니메이터 초기화(트리거/상태 재설정)
        if (animator)
        {
            animator.ResetTrigger(HashHit);
            animator.ResetTrigger(HashDie);
            animator.Rebind();        // 바인딩 리셋
            animator.Update(0f);      // 즉시 반영
        }
    }

    // ★추가: UI 반영을 한 곳에서
    private void UpdateHpUI()
    {
        if (!back) return;
        float ratio = (Fullhealth <= 0f) ? 0f : Nowhealth / Fullhealth;
        back.fillAmount = Mathf.Clamp01(ratio);
    }
}
