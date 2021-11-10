using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Jump State", menuName = "CharacterState/Jump State")]
public class JumpAbility : StateData
{
    private static Animator anim;
    void InputDetect()
    {
        //Debug.Log("jump");
        anim.SetTrigger(AnimParms.Jump);
        EventCenter.GetInstance().RemoveEventListener(EventName.jumpbuttonclicked, InputDetect);
    }


    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        anim = animator;
        EventCenter.GetInstance().AddEventListener(EventName.jumpbuttonclicked, InputDetect);
        InputMgr.GetInstance().EnableKeyDetect(true);
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.jumpbuttonclicked, InputDetect);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
