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


    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            UIManager.GetInstance().ShowPanel<InteractiveButtonPanel>(PanelName.InteractiveButtonPanel);
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
        }
    }
}
