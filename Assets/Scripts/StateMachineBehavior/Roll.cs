using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RollDetect", menuName = "CharacterState/RollDetect")]
public class Roll : StateData
{
    void Rolldetect()
    {
        Debug.Log("jump");
        CharacterState.GetAnimator().SetTrigger(AnimParms.Roll);
        EventCenter.GetInstance().RemoveEventListener(EventName.rollbuttonclicked, Rolldetect);
    }

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.GetInstance().AddEventListener(EventName.rollbuttonclicked, Rolldetect);
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.rollbuttonclicked, Rolldetect);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
