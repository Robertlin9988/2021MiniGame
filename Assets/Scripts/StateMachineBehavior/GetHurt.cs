using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHurt : StateMachineBehaviour
{
    public float dizzletime = 1;
    public bool playfx = false;
    private GameObject fx;
    private float currenttime = 0;
    private bool resumed = false;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        AudioManager.GetInstance().PlaySFXAtPoint(AudiosName.playerhurt, animator.transform.position);
        CharacterState.GetThirdPersonControl().canmove = false;
        currenttime = 0;
        resumed = false;
        if(playfx)
        {
            fx = GlobalFxManager.GetInstance().PlayandGetVFX(animator.transform.position + Vector3.up*2, Quaternion.LookRotation(Vector3.up), VFXName.Dizzle);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        CharacterState.GetThirdPersonControl().canmove = true;
        if (playfx&&fx!=null)
        {
            PoolMgr.GetInstance().PushObj(fx.name, fx);
        }
    }

    /// <summary>
    /// 每个update调用
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="animatorStateInfo"></param>
    /// <param name="layerIndex"></param>
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(currenttime>=dizzletime&&!resumed)
        {
            CharacterState.GetThirdPersonControl().canmove = true;
            animator.SetTrigger(AnimParms.resume);
            resumed = true;
        }
        currenttime += Time.deltaTime;
    }
}
