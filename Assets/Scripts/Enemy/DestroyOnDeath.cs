using UnityEngine;

public class DestroyOnDeath : StateMachineBehaviour
{
    [SerializeField] private bool destroyIfNoSpawner = true;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ★ 루트 Transform을 사용
        var root = animator.transform.root;

        // 스포너를 부모 쪽까지 탐색 (프리팹/스폰포인트 구조 모두 대응)
        var spawner = animator.GetComponentInParent<SpawnSlime>();
        if (spawner != null)
        {
            // ★ 루트를 넘겨서 스포너가 정확히 부모 오브젝트를 파괴하도록
            spawner.HandleDeathAndRespawn(root);
        }
        else if (destroyIfNoSpawner)
        {
            Object.Destroy(root.gameObject);  // ★ 루트를 파괴
        }
    }
}
