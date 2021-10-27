using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class getkey : Interactive
{


    public PlayableDirector withkey;

    void Getkey()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, Getkey);
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        TimelineManager.GetInstance().PlayTimeline(withkey);
        openbox.haskey = true;
        Destroy(this.gameObject);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, Getkey);
        }
    }


    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
