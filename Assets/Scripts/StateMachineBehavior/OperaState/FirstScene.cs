using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstScene : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TimelineManager.GetInstance().PlayTimeline(timelinename.WakeUp.ToString());
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
