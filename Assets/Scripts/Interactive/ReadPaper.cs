using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class ReadPaper : Interactive
{
    public PlayableDirector anim;
    private bool papershow=false;

    public void Setpapershow(bool state)
    {
        papershow = state;
    }


    void PaperShow()
    {
        //动画未播放完不响应按键
        if(!papershow && anim.state!=PlayState.Playing)
        {
            Debug.Log(gameObject.name+" Play!");
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, PaperShow);
            //CharacterState.GetThirdPersonControl().canmove = false;
            UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
            TimelineManager.GetInstance().PlayTimeline(anim);
            papershow = true;
        }
    }

    private void Update()
    {
        if (papershow)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (anim.playableGraph.GetRootPlayable(0).GetSpeed() <= 0.01d)
                {
                    //CharacterState.GetThirdPersonControl().canmove = false;
                    TimelineManager.GetInstance().ResumeTimeLine(anim);
                    papershow = false;
                }
            }
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("enter");
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, PaperShow);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("exit");
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, PaperShow);
        }
    }
}
