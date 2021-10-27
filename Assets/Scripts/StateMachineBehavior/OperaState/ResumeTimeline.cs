using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeTimeline : StateMachineBehaviour
{
    public enum resumestate
    {
        resumeonenter, resumeonexit
    }
    public resumestate state;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(state==resumestate.resumeonenter)
            TimelineManager.GetInstance().ResumeTimeLine();
    }


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(state == resumestate.resumeonexit)
            TimelineManager.GetInstance().ResumeTimeLine();
    }
}
