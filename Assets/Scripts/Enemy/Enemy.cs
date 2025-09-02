using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    private static readonly int HashDie = Animator.StringToHash("Die");

    private bool isDying;

    private void Start()
    {
        UpdateHpUI();
    }

    // ���߰�: ���� �����ǰų� SetActive(true) �� �� �ڵ� �ʱ�ȭ
    private void OnEnable()
    {
        ResetForSpawn();   // �� ������ �� �׻� ȣ���
    }

    public void Update() // Ŭ�� �̺�Ʈ���� ��ä�� ������ �ӽ� �׽�Ʈ��

    {
        if (isDying)return;


        if (Input.GetMouseButtonDown(0))
        {

            IsDamaged();
            Debug.Log("�ƾ�");

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
            //if (back) back.fillAmount = ratio;              // �� fillAmount�� �ݿ�
            UpdateHpUI();
            animator.SetTrigger(HashHit);
        }
        else
        {
            //front.localScale = new Vector3(0.0f, 1.0f);//�� ���Ʈ�� ��ü
            Nowhealth = 0f;
            UpdateHpUI();                 // �� 0���� ����

            IsDie();
        }
        
    }
    public void IsDie()
    {
        if (isDying)return;

        isDying = true;
        animator.SetTrigger(HashDie);
        Debug.Log("�����ֱ�");
    }

    // ���߰�: HP��/�ִϸ�����/�÷��׸� ������ ������ �����·�
    private void ResetForSpawn()
    {
        isDying = false;              // �� ������ �κ�: ���������� false�� �ʱ�ȭ
        Nowhealth = Fullhealth;       // Ǯ ü��

        // HP UI
        UpdateHpUI();

        // �ִϸ����� �ʱ�ȭ(Ʈ����/���� �缳��)
        if (animator)
        {
            animator.ResetTrigger(HashHit);
            animator.ResetTrigger(HashDie);
            animator.Rebind();        // ���ε� ����
            animator.Update(0f);      // ��� �ݿ�
        }
    }

    // ���߰�: UI �ݿ��� �� ������
    private void UpdateHpUI()
    {
        if (!back) return;
        float ratio = (Fullhealth <= 0f) ? 0f : Nowhealth / Fullhealth;
        back.fillAmount = Mathf.Clamp01(ratio);
    }
}
