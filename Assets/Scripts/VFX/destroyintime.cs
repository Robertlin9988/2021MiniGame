using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyintime : MonoBehaviour
{

    private void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void push()
    {
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }

    void OnParticleSystemStopped()
    {
        PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }
}
