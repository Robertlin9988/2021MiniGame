using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : Interactive
{
    //��Ӧ���ŵĶ���
    public Animator m_anim;

    void OpenDoor()
    {
        m_anim.SetTrigger(AnimParms.triggerdoor);
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, OpenDoor);
        }
        else if(other.gameObject.tag=="enemy")
        {
            OpenDoor();
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, OpenDoor);
        }
    }
}
