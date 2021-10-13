using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : StateMachineBehaviour
{
    [Header("能力列表")]
    public List<StateData> ListAbilityData = new List<StateData>();

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
}
