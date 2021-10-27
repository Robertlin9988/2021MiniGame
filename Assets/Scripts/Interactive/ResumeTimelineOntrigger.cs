using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeTimelineOntrigger : MonoBehaviour
{
    public enum resumestate
    {
        resumeonenter, resumeonexit
    }
    public resumestate state;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            if (state == resumestate.resumeonenter)
                TimelineManager.GetInstance().ResumeTimeLine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (state == resumestate.resumeonexit)
                TimelineManager.GetInstance().ResumeTimeLine();
        }
    }
}
