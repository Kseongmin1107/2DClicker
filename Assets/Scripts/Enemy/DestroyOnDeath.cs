using UnityEngine;

public class DestroyOnDeath : StateMachineBehaviour
{
    // 죽음 애니메이션이 끝났을 때 호출됨
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.StageManager.CheckWave();

    }
}
