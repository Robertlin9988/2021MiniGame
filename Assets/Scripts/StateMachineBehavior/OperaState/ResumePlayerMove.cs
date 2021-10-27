using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumePlayerMove : StateMachineBehaviour
{
    public enum resumestate
    {
        resumeonenter, resumeonexit
    }
    public resumestate state;

    public bool ifstatetrans=true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (state == resumestate.resumeonenter)
            EventCenter.GetInstance().EventTrigger(EventName.dialogfinish);
        if (ifstatetrans)
        {
            animator.SetTrigger(AnimParam.statetrans);
        }
        OperaManager.GetInstance().ResumeMainDialog();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (state == resumestate.resumeonexit)
            EventCenter.GetInstance().EventTrigger(EventName.dialogfinish);
    }
}
