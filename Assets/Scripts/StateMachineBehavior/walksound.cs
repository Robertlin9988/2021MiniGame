using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Walksound", menuName = "CharacterState/Walksound")]
public class walksound : StateData
{
    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetFloat("walkstep")>=0.88f)
            AudioManager.GetInstance().PlaySFX(AudiosName.walkstep,false);
        else
            AudioManager.GetInstance().StopSound(AudiosName.walkstep);
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioManager.GetInstance().StopSound(AudiosName.walkstep);
    }
}
