using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Words : MonoBehaviour
{
    private GameObject shadow;
    private string shadowpath = "ShadowCircleBig";
    private string collisioneffect = "VFXs/SoftBodySlam";
    private string hitplayereffect = "VFXs/RoundHitYellow";
    private Vector3 offset = new Vector3(1.7f, 0, 0);
    public float damageperhit = 4;

    private void Onhitplayer()
    {
        PoolMgr.GetInstance().PushObj(shadow.name, shadow);
        PoolMgr.GetInstance().GetObj(hitplayereffect, transform.position, Quaternion.LookRotation(Vector3.up), (o) => { });
        //PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
        shadow = null;
    }



    private void OnEnable()
    {
        GetComponent<BoxCollider>().enabled = true;
        Vector3 pos = transform.position;
        transform.position += offset;
        PoolMgr.GetInstance().GetObj(shadowpath, (o) => {
            o.transform.position = new Vector3(pos.x, 0.01f, pos.z);
            shadow = o;
        });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Floor"&&shadow!=null)
        {
            PoolMgr.GetInstance().PushObj(shadow.name, shadow);
            PoolMgr.GetInstance().GetObj(collisioneffect, new Vector3(transform.position.x - offset.x, 0.01f, transform.position.z - offset.z), Quaternion.LookRotation(Vector3.up), (o) => { });
            AudioManager.GetInstance().PlaySFXAtPoint(AudiosName.fallhit, transform.position - offset);
            shadow = null;
        }
        else if (collision.gameObject.tag == "Player")
        {
            Onhitplayer();
            EventCenter.GetInstance().EventTrigger<float>(EventName.playerhurt, damageperhit);
        }
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    GetComponent<BoxCollider>().enabled = false;
        //    PoolMgr.GetInstance().GetObj(this.gameObject.name + "ห้", transform.position, Quaternion.identity, (o) =>
        //    {
        //    });
        //    PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
        //}
    }
}
