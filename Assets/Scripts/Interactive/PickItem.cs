using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : Interactive
{
    void SetPlayerArmed(Collider other)
    {
        Destroy(this.gameObject);
        other.GetComponent<PlayerAbility>().SetArmed();
    }


    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.tag == "Player")
        {
            EventCenter.GetInstance().AddEventListener<Collider>(EventName.interactivebuttonclicked, SetPlayerArmed);
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
            EventCenter.GetInstance().RemoveEventListener<Collider>(EventName.interactivebuttonclicked, SetPlayerArmed);
        }
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<Collider>(EventName.interactivebuttonclicked, SetPlayerArmed);
    }
}
