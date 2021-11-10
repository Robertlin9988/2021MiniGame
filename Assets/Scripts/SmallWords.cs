using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallWords : MonoBehaviour
{
    private Rigidbody myrigid;
    private GameObject shadow;
    private string shadowpath = "ShadowCircle";
    private string collisioneffect = "VFXs/SoftBodySlamSmall";
    private string hitplayereffect = "VFXs/RoundHitYellow";
    private Vector3 offset = new Vector3(0.17f, 0, 0);
    public float damageperhit=1;


    private void Onhitfloor()
    {
        PoolMgr.GetInstance().PushObj(shadow.name, shadow);
        PoolMgr.GetInstance().GetObj(collisioneffect, new Vector3(transform.position.x - offset.x, 0.01f, transform.position.z - offset.z), Quaternion.LookRotation(Vector3.up), (o) => { });
        AudioManager.GetInstance().PlaySFXAtPoint(AudiosName.fallhit, transform.position - offset);
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
        shadow = null;
    }

    private void Onhitplayer()
    {
        PoolMgr.GetInstance().PushObj(shadow.name, shadow);
        PoolMgr.GetInstance().GetObj(hitplayereffect, transform.position, Quaternion.LookRotation(Vector3.up), (o) => { });
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
        shadow = null;
    }


    private void OnEnable()
    {
        Vector3 pos = transform.position;
        transform.position += offset;
        PoolMgr.GetInstance().GetObj(shadowpath, (o) => {
            o.transform.position = new Vector3(pos.x, 0.01f, pos.z);
            shadow = o;
        });
    }

    private void OnDisable()
    {
        myrigid.velocity = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && shadow != null)
        {
            Onhitfloor();
        }
        else if(collision.gameObject.tag=="Player")
        {
            Onhitplayer();
            EventCenter.GetInstance().EventTrigger<float>(EventName.playerhurt, damageperhit);
        }
    }

    private void Awake()
    {
        myrigid = GetComponent<Rigidbody>();
    }

    //·ÀÖ¹´©Ä£
    private void Update()
    {
        if(transform.position.y<-1.0f && shadow != null)
        {
            Onhitfloor();
        }
    }
}
