using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum bulletcolor
{
    TriangleMissileBlue,
    TriangleMissileGreen,
    TriangleMissileRed
}

public enum bulletcharge
{
    ChargeBlue,
    ChargeGreen,
    ChargeRed
}

public enum bulletimpact
{
    TriangleExplosionBlue,
    TriangleExplosionGreen,
    TriangleExplosionRed
}

[Serializable]
public struct bullettype
{
    public bulletcharge charge;
    public bulletcolor color;
    public bulletimpact impact;

    public bullettype(bulletcharge chargeBlue, bulletcolor triangleMissileBlue, bulletimpact triangleExplosionBlue) : this()
    {
        this.charge = chargeBlue;
        this.color = triangleMissileBlue;
        this.impact = triangleExplosionBlue;
    }
}



public class Bullets : MonoBehaviour
{
    Rigidbody myrigidbody;
    float currenttime;
    float alivetime;
    public bullettype type;
    public GameObject trial;
    public float damage=1;

    void PushObj()
    {
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }

    public void Reintial(float speed,float time,bool usegracity)
    {
        myrigidbody.useGravity = usegracity;
        myrigidbody.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        alivetime = time;
        trial.SetActive(true);
    }


    private void Awake()
    {
        myrigidbody = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        currenttime = 0;
        myrigidbody.velocity = Vector3.zero;
        trial.SetActive(false);
    }

    private void Update()
    {
        currenttime += Time.deltaTime;
        if(currenttime>alivetime)
        {
            PushObj();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitpos = transform.position;
        myrigidbody.velocity = Vector3.zero;
        GlobalFxManager.GetInstance().PlayfxOnceAtPoint(hitpos, Quaternion.identity, type.impact.ToString());
        if(collision.gameObject.tag=="enemy")
        {
            int index;
            switch(type.color)
            {
                case bulletcolor.TriangleMissileBlue:
                    index = 2;
                    break;
                case bulletcolor.TriangleMissileRed:
                    index = 0;
                    break;
                default:
                    index = 1;
                    break;
            }
            EventCenter.GetInstance().EventTrigger<float,int>(EventName.bullethit, damage,index);
        }

        PushObj();
    }
}
