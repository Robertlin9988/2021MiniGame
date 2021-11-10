using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFxManager : BaseManager<GlobalFxManager>
{
    private Dictionary<string, string> fxs=new Dictionary<string, string>();


    public GlobalFxManager()
    {
        Particles o = Resources.Load("Particles") as Particles;
        foreach (ParticleInfo fx in o.PlayerSFX)
        {
            fxs.Add(fx.name, fx.particlefxfile);
        }
    }

    public void PlayfxOnceAtPoint(Vector3 position, Quaternion rotation, string name)
    {
        if (!fxs.ContainsKey(name)) return;
        PoolMgr.GetInstance().GetObj(fxs[name], (o) => {
            o.transform.position = position;
            o.transform.rotation = rotation;
        });
    }

    public void PlayBulletAtPoint(Vector3 position, Quaternion rotation, string name,float speed,float time,bool usegravity)
    {
        if (!fxs.ContainsKey(name)) return;
        PoolMgr.GetInstance().GetObj(fxs[name], (o) => {
            o.transform.position = position;
            o.transform.rotation = rotation;
            o.GetComponent<Bullets>().Reintial(speed,time,usegravity);
        });
    }

    public void PlayfxAttachedToTransform(Transform parent,Vector3 position, Quaternion rotation,string name)
    {
        if (!fxs.ContainsKey(name)) return;
        PoolMgr.GetInstance().GetObj(fxs[name], (o) => {
            o.transform.SetPositionAndRotation(position, rotation);
            o.transform.parent = parent;
        });
    }

    public GameObject PlayandGetVFX(Vector3 position, Quaternion rotation, string name)
    {
        GameObject obj = null;
        if (!fxs.ContainsKey(name)) return obj;
        PoolMgr.GetInstance().GetObj(fxs[name], (o) => {
            o.transform.position = position;
            o.transform.rotation = rotation;
            obj = o;
        });
        return obj;
    }
}



