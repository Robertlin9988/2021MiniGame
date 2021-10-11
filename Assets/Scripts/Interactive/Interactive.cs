using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ��������ÿ�����в�ͬ�Ĺ���
/// ���°�ť�����¼��б�
/// </summary>
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
        if (istrigger && Input.GetKey(PanelName.interactivekey))
        {
            UIManager.GetInstance().HidePanel(PanelName.InteractiveButtonPanel);
            EventCenter.GetInstance().EventTrigger<Collider>(EventName.interactivebuttonclicked,other);
        }
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
