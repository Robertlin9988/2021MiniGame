using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FirsrSceneFinish : StateMachineBehaviour
{

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject.Find("Aisha").GetComponent<ThirdCharacterController>().SetPosition(new Vector3(-14.3f, -15.739f, -5.87f));
        TimelineManager.GetInstance().PlayTimeline(timelinename.MirrorBreak1.ToString());
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
