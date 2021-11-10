using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "fire State", menuName = "CharacterState/fire State")]
public class Fire : StateData
{

    void fire()
    {
        bool success=CharacterState.GetShootAbility().Shoot();
        if(success)
        {
            EventCenter.GetInstance().RemoveEventListener(EventName.playeredfired, fire);
            EventCenter.GetInstance().RemoveEventListener(EventName.playeraimed, CancelAim);
            CharacterState.GetAnimator().SetTrigger(AnimParms.fire);
        }
        else
        {
            EventCenter.GetInstance().EventTrigger(EventName.runoutofbullets);
        }
    }

    void CancelAim()
    {
        CharacterState.GetShootAbility().CancelAim();
        Animevents.GetInstance().bowtriggerexit();
        CharacterState.GetAnimator().SetBool(AnimParms.aiming, false);
        CharacterState.GetThirdPersonControl().isaiming = false;
        CMSwitcher.GetInstance().SwitchCM();

        UIManager.GetInstance().HidePanel(PanelName.aimpanel);
        EventCenter.GetInstance().RemoveEventListener(EventName.playeraimed, CancelAim);
        EventCenter.GetInstance().RemoveEventListener(EventName.playeredfired, fire);
    }

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        EventCenter.GetInstance().AddEventListener(EventName.playeredfired, fire);
        EventCenter.GetInstance().AddEventListener(EventName.playeraimed, CancelAim);
        Animevents.GetInstance().bowtriggerstart();
        CharacterState.GetShootAbility().Aim();
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Animevents.GetInstance().bowtriggerexit();
        CharacterState.GetShootAbility().CancelAim();
        EventCenter.GetInstance().RemoveEventListener(EventName.playeredfired, fire);
        EventCenter.GetInstance().RemoveEventListener(EventName.playeraimed, CancelAim);
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
