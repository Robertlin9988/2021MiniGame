using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBullet : MonoBehaviour
{
    public bullettype type;
    public float chargenum = 8;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            UIManager.GetInstance().ShowPanel<BulletPanel>(PanelName.bulletpanel);
            AudioManager.GetInstance().PlaySFXAtPoint(AudiosName.charged,transform.position,1);
            EventCenter.GetInstance().EventTrigger<bullettype,float>(EventName.pickupbullets,type,chargenum);
        }
    }
}
