using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : Interactive
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
        if(istrigger && Input.GetKey(PanelName.interactivekey))
        {
            UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
            Destroy(this.gameObject);
            other.GetComponent<PlayerAbility>().armed = true;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
