using UnityEngine;

public class DestroyOnDeath : StateMachineBehaviour
{
    // ���� �ִϸ��̼��� ������ �� ȣ���
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //GameManager.Instance.StageManager.CheckWave();

    }
}
