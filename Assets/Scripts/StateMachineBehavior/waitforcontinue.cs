using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waitforcontinue : StateMachineBehaviour
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Input.GetKeyDown(PanelName.continuekey))
        {
            AudioManager.GetInstance().PlaySFX(AudiosName.continuebutton);
            animator.SetTrigger("statetrans");
        }
    }
}
