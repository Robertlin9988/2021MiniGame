using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : Interactive
{
    void SetPlayerArmed()
    {
        EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked,SetPlayerArmed);
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool(AnimParms.Pickup, true);
        UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
        gameObject.SetActive(false);
    }

    void PutItem(Vector3 pos)
    {
        gameObject.transform.position = pos;
        gameObject.SetActive(true);
    }


    public override void OnTriggerEnter(Collider other)
    {
        //Debug.Log("pickle item enter");
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener(EventName.interactivebuttonclicked, SetPlayerArmed);
            EventCenter.GetInstance().AddEventListener<Vector3>(EventName.enemypatroldisturbance, PutItem);
        }
    }

    public override void OnTriggerStay(Collider other)
    {
        //Debug.Log("pickle item stay");
        base.OnTriggerStay(other);
    }

    public override void OnTriggerExit(Collider other)
    {
        //Debug.Log("pickle item exit");
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().RemoveEventListener(EventName.interactivebuttonclicked, SetPlayerArmed);
        }
    }
}
