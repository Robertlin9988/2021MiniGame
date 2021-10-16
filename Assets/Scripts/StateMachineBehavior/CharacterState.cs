using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : StateMachineBehaviour
{
    [Header("能力列表")]
    public List<StateData> ListAbilityData = new List<StateData>();

    private static ThirdPersonPlayerMovement playercontroller;
    private static Animator anim;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (anim == null) anim = animator;
        if (playercontroller == null) playercontroller = animator.gameObject.GetComponent<ThirdPersonPlayerMovement>();

        foreach (StateData d in ListAbilityData)
        {
            d.OnEnter(this, animator, stateInfo, layerIndex);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData d in ListAbilityData)
        {
            d.UpdateAbility(this, animator, stateInfo, layerIndex);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (StateData d in ListAbilityData)
        {
            d.OnExit(this, animator, stateInfo, layerIndex);
        }
    }

    public static ThirdPersonPlayerMovement GetPlayerController()
    {
        if (playercontroller == null) Debug.LogError("playercontroller not found!");
        return playercontroller;
    }

    public static Animator GetAnimator()
    {
        if(anim==null) Debug.LogError("AnimatorController not found!");
        return anim;
    }
}
