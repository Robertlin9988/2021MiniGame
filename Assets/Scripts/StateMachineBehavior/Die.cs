using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TimelineManager.GetInstance().PlayTimeline(timelinename.Die.ToString());
        animator.SetBool("Dying", true);
    }
}
