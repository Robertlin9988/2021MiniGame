using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : Interactive
{
    private Animator anim;
    private bool active = false;

    public Vector3 oripos;

    private void Awake()
    {
        oripos = transform.position;
    }


    void SetPlayerArmed()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked,SetPlayerArmed);
        anim.SetBool(AnimParms.Pickup, true);
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        gameObject.SetActive(false);
    }

    void PutItem(Vector3 pos)
    {
        active = true;
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
        EventCenter.GetInstance().RemoveEventListener<Vector3>(EventName.enemypatroldisturbance, PutItem);
    }

    void ResetPos()
    {
        active = false;
        gameObject.transform.position = oripos;
        gameObject.SetActive(true);
        EventCenter.GetInstance().RemoveEventListener(EventName.enemyarrivedisturbance, ResetPos);
    }


    public override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("pickle item enter")
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("armed"))
        {
            base.OnTriggerEnter(other);
            anim = other.gameObject.GetComponent<Animator>();
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, SetPlayerArmed);
            EventCenter.GetInstance().AddEventListener<Vector3>(EventName.enemypatroldisturbance, PutItem);
        }
        else if(other.gameObject.tag=="enemy"&& active)
        {
            EventCenter.GetInstance().EventTrigger<Vector3>(EventName.enemypatroldisturbance,oripos);
            EventCenter.GetInstance().AddEventListener(EventName.enemyarrivedisturbance, ResetPos);
            gameObject.SetActive(false);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        //Debug.Log("pickle item exit");
        if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("armed"))
        {
            base.OnTriggerExit(other);
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetPlayerArmed);
        }
    }
}
