using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class opendoor : Interactive
{
    //对应的门的动画
    public Animator m_anim;
    public bool playercanopen=false;
    public float closetome;

    private bool dooropened = false;
    private float currentopentime;

    /// <summary>
    /// 玩家按键开门事件
    /// </summary>
    /// <param name="other"></param>
    void OpenDoor()
    {
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        m_anim.SetTrigger(AnimParms.triggerdoor);
        dooropened = !dooropened;
        currentopentime = 0;
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, OpenDoor);
    }

    /// <summary>
    /// npc开门
    /// </summary>
    void EnemyOpen()
    {
        m_anim.SetTrigger(AnimParms.triggerdoor);
        dooropened = true;
        currentopentime = 0;
    }

    void CloseDoor()
    {
        m_anim.SetTrigger(AnimParms.triggerdoor);
        dooropened = false;
        currentopentime = 0;
    }

    public override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("open door enter");
        if (other.gameObject.tag == "Player" && playercanopen)
        {
            base.OnTriggerEnter(other);
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, OpenDoor);
        }
        else if(other.gameObject.tag=="enemy")
        {
            if(!dooropened)
                EnemyOpen();
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        //Debug.Log("open door exit");
        if (other.gameObject.tag == "Player" && playercanopen)
        {
            base.OnTriggerExit(other);
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, OpenDoor);
        }
    }

    private void Update()
    {
        if(dooropened)
        {
            m_anim.transform.GetComponent<MeshCollider>().isTrigger = true;
            currentopentime += Time.deltaTime;
            if(currentopentime> closetome)
            {
                CloseDoor();
            }
        }
        else
        {
            m_anim.transform.GetComponent<MeshCollider>().isTrigger = false;
        }
    }
}
