using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnce : StateMachineBehaviour
{
    public SoundsInfo info;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.GetInstance().PlaySFXAtPoint(info.soundname.ToString(), animator.transform.position, info.volume);
    }
}
