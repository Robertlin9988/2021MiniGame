using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateData : ScriptableObject
{
    public abstract void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    public abstract void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
    public abstract void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex);
}
