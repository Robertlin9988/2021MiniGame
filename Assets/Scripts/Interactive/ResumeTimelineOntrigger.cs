using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeTimelineOntrigger : MonoBehaviour
{

    public timelinename timelinename;

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
                TimelineManager.GetInstance().ResumeTimeline(timelinename.ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (state == resumestate.resumeonexit)
                TimelineManager.GetInstance().ResumeTimeline(timelinename.ToString());
        }
    }
}
