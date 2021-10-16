using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crouch State", menuName = "CharacterState/Crouch State")]
public class CrouchState : StateData
{
    private static Animator anim;

    void InputDetect()
    {
        anim.SetBool(AnimParms.IsCouching, false);
    }


    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        EventCenter.GetInstance().AddEventListener(EventName.crouchbuttonclicked, InputDetect);
        anim.SetBool(AnimParms.IsCouching, true);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.crouchbuttonclicked, InputDetect);
        anim.SetBool(AnimParms.IsCouching, false);
    }
}
