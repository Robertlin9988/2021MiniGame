using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Jumpstate", menuName = "CharacterState/Jumpstate")]
public class JumpState : StateData
{

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterState.GetThirdPersonControl().isjump = true;
        CharacterState.GetThirdPersonControl().movespeed *= 2;
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterState.GetThirdPersonControl().isjump = false;
        CharacterState.GetThirdPersonControl().movespeed /= 2;
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
