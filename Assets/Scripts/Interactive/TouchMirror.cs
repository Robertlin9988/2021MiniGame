using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TouchMirror : Interactive
{
    public PlayableDirector opera;


    void SetaOperaOn()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetaOperaOn);
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        TimelineManager.GetInstance().PlayTimeline(opera);
    }


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, SetaOperaOn);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetaOperaOn);
        }
    }
}
