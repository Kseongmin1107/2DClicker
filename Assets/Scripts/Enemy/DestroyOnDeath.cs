using UnityEngine;

public class DestroyOnDeath : StateMachineBehaviour
{
    [SerializeField] private bool destroyIfNoSpawner = true;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // �� ��Ʈ Transform�� ���
        var root = animator.transform.root;

        // �����ʸ� �θ� �ʱ��� Ž�� (������/��������Ʈ ���� ��� ����)
        var spawner = animator.GetComponentInParent<SpawnSlime>();
        if (spawner != null)
        {
            // �� ��Ʈ�� �Ѱܼ� �����ʰ� ��Ȯ�� �θ� ������Ʈ�� �ı��ϵ���
            spawner.HandleDeathAndRespawn(root);
        }
        else if (destroyIfNoSpawner)
        {
            Object.Destroy(root.gameObject);  // �� ��Ʈ�� �ı�
        }
    }
}
