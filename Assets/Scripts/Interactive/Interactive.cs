using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    protected bool istrigger = false;
    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            UIManager.GetInstance().ShowPanel<InteractiveButtonPanel>(PanelName.InteractiveButtonPanel);
            istrigger = true;
        }
    }

    public virtual void OnTriggerStay(Collider other)
    {
        
    }


    public virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
            istrigger = false;
        }
    }
}
