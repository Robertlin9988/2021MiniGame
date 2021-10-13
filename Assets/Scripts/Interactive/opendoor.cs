using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : Interactive
{
    //��Ӧ���ŵĶ���
    public Animator m_anim;

    /// <summary>
    /// ��Ұ��������¼�
    /// </summary>
    /// <param name="other"></param>
    void OpenDoor()
    {
        m_anim.SetTrigger(AnimParms.triggerdoor);
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, OpenDoor);
    }

    /// <summary>
    /// npc����
    /// </summary>
    void EnemyOpen()
    {
        m_anim.SetTrigger(AnimParms.triggerdoor);
    }

    public override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("open door enter");
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, OpenDoor);
        }
        else if(other.gameObject.tag=="enemy")
        {
            EnemyOpen();
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        //Debug.Log("open door stay");
        base.OnTriggerStay(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        //Debug.Log("open door exit");
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, OpenDoor);
        }
        else if (other.gameObject.tag == "enemy")
        {
            EnemyOpen();
        }
    }
}
