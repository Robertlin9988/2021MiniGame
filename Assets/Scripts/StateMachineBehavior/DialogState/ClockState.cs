using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering.PostProcessing;

public class ClockState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //AudioManager.GetInstance().PlaySFX(AudiosName.clock, true, 1);
        //AudioManager.GetInstance().PlaySFX(AudiosName.wakeup, false, 1);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        //AudioManager.GetInstance().StopSound(AudiosName.clock);
        //AudioManager.GetInstance().StopSound(AudiosName.wakeup);
    }
}
