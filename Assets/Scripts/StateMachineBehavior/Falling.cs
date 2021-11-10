using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if(CharacterState.GetThirdPersonControl()!=null)
            CharacterState.GetThirdPersonControl().isfall = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (CharacterState.GetThirdPersonControl() != null)
            CharacterState.GetThirdPersonControl().isfall = false;
    }
}
