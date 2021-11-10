using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject shootpos;
    public float bulletspeed=100;
    public float bulletalivetime = 5;
    public bool usegravity = true;

    float bulletnums=0;
    GameObject currentaimfx;
    Cinemachine.CinemachineImpulseSource impusesource;
    bullettype currenttype=new bullettype(bulletcharge.ChargeBlue,bulletcolor.TriangleMissileBlue,bulletimpact.TriangleExplosionBlue);
    

    public void Charged(bullettype type,float num)
    {
        bulletnums = num;
        currenttype = type;
    }


    public bool Shoot()
    {
        impusesource = GetComponent<Cinemachine.CinemachineImpulseSource>();
        impusesource.GenerateImpulse(Camera.main.transform.forward);


        //如果子弹为零则触发ui及音效事件
        if (bulletnums == 0)
        {
            Debug.Log("runoutofbullets");
            EventCenter.GetInstance().EventTrigger(EventName.runoutofbullets);
            return false;
        }


        CancelAim();

        bulletnums--;
        //触发事件
        EventCenter.GetInstance().EventTrigger<float>(EventName.successshoot,bulletnums);

        float maxdistance = bulletspeed * bulletalivetime;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetpoint;
        if(Physics.Raycast(ray,out hit,maxdistance))
        {
            targetpoint = hit.point;
        }
        else
        {
            targetpoint = Camera.main.transform.position + Camera.main.transform.forward* maxdistance;
        }
        Vector3 shootingray = targetpoint - shootpos.transform.position;
        GlobalFxManager.GetInstance().PlayBulletAtPoint(shootpos.transform.position, Quaternion.LookRotation(shootingray), currenttype.color.ToString(), bulletspeed,bulletalivetime,usegravity);
        return true;
    }

    public void Aim()
    {
        if(bulletnums>0)
        {
            currentaimfx = GlobalFxManager.GetInstance().PlayandGetVFX(shootpos.transform.position, shootpos.transform.rotation, currenttype.charge.ToString());
        }
    }

    public void CancelAim()
    {
        if (currentaimfx != null)
        {
            PoolMgr.GetInstance().PushObj(currentaimfx.name, currentaimfx);
            currentaimfx = null;
        }
    }

    private void Start()
    {
        //Cursor.visible = false;
        EventCenter.GetInstance().AddEventListener<bullettype, float>(EventName.pickupbullets, Charged);
    }

    private void Update()
    {
        if(currentaimfx!=null)
        {
            currentaimfx.transform.position = shootpos.transform.position;
            currentaimfx.transform.rotation = shootpos.transform.rotation;
        }
    }
}
