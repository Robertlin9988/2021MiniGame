using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ReadPaper : Interactive
{
    public PlayableDirector anim;
    private ThirdCharacterController playercontroller;

    void SetPaperShow()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetPaperShow);
        EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, SetPaperHide);
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        TimelineManager.GetInstance().PlayTimeline(anim);
        playercontroller.SetCanmove(false);
    }

    void SetPaperHide()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetPaperHide);
        TimelineManager.GetInstance().ResumeTimeLine();
        playercontroller.SetCanmove(true);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, SetPaperShow);
            playercontroller = other.gameObject.GetComponent<ThirdCharacterController>();
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetPaperShow);
        }
    }
}
