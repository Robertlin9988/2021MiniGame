using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Dialogname
{
    Boss,npc0
}




public class TalktoNPC : Interactive
{
    public Dialogname dialogname;
    private ThirdCharacterController playercontroller;

    void Resumeplayermovement()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.dialogfinish, Resumeplayermovement);
        playercontroller.SetCanmove(true);
    }



    void SetTalk()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetTalk);
        EventCenter.GetInstance().AddEventListener(EventName.dialogfinish, Resumeplayermovement);
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        OperaManager.GetInstance().PlayDialog(dialogname.ToString());
        playercontroller.SetCanmove(false);
    }


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, SetTalk);
            playercontroller = other.gameObject.GetComponent<ThirdCharacterController>();
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetTalk);
        }
    }
}
