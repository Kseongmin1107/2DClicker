using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public RectTransform front;


    public float Fullhealth = 100.0f;//���߿� �����Ӻ��� �ٸ� ü���� ���� �� �ֵ��� ����
    public float Nowhealth = 100.0f;//�������� ������ �� ���� �ִ� ü�°� ���� �����ϰ� �ٲ�
    public float Hitdamage = 5; //�� ���� ���߿� ���ݷ��� �޾ƿ��°ɷ� ��ü

    private Animator animator;
    public void Update() // Ŭ�� �̺�Ʈ���� ��ä�� ������ �ӽ� �׽�Ʈ��
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsDamaged();

        }
    }
    public void IsDamaged()
    {
        Nowhealth = Nowhealth - Hitdamage;
        if (Nowhealth > 0)
        {
            front.localScale = new Vector3(Nowhealth / Fullhealth, 1.0f);
        }
        else
        {
            front.localScale = new Vector3(0.0f, 1.0f);//�� ���Ʈ�� ��ü
            IsDie();
        }
        
    }
    public void IsDie()
    {
        Debug.Log("�����ֱ�");
    }


}
