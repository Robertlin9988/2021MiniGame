using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class openbox : Interactive
{
    public static bool haskey;
    public PlayableDirector nokey;
    public PlayableDirector withkey;


    void Open()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, Open);
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        if(haskey)
        {
            TimelineManager.GetInstance().PlayTimeline(withkey);
        }
        else
        {
            TimelineManager.GetInstance().PlayTimeline(nokey);
        }
    }


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, Open);
        }
    }


    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
