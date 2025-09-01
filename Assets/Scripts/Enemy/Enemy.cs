using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public RectTransform front;


    public float Fullhealth = 100.0f;//���߿� �����Ӻ��� �ٸ� ü���� ���� �� �ֵ��� ����
    public float Nowhealth = 100.0f;//�������� ������ �� ���� �ִ� ü�°� ���� �����ϰ� �ٲ�
    public float Hitdamage = 5; //�� ���� ���߿� ���ݷ��� �޾ƿ��°ɷ� ��ü


    [SerializeField] private Animator animator;
    private static readonly int HashHit = Animator.StringToHash("Hit");
    private static readonly int HashDie = Animator.StringToHash("Die");

    private bool isDying;

    public void Update() // Ŭ�� �̺�Ʈ���� ��ä�� ������ �ӽ� �׽�Ʈ��

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

            front.localScale = new Vector3(Nowhealth / Fullhealth, 1.0f);
            animator.SetTrigger(HashHit);
        }
        else
        {
            front.localScale = new Vector3(0.0f, 1.0f);//�� ���Ʈ�� ��ü
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
}
