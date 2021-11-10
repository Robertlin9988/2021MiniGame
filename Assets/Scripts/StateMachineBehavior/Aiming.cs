using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Aiming State", menuName = "CharacterState/Aim State")]
public class Aiming : StateData
{
    void Aimtarget()
    {
        CharacterState.GetAnimator().SetBool(AnimParms.aiming, true);
        CharacterState.GetThirdPersonControl().isaiming = true;
        CMSwitcher.GetInstance().SwitchCM();
        UIManager.GetInstance().ShowPanel<AimPanel>(PanelName.aimpanel);
        EventCenter.GetInstance().RemoveEventListener(EventName.playeraimed, Aimtarget);
    }


    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.GetInstance().AddEventListener(EventName.playeraimed, Aimtarget);
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.playeraimed, Aimtarget);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
