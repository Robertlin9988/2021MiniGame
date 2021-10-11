using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class enemyanim : AnimatorController
{
    NavMeshAgent navmeshagent;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        navmeshagent = GetComponent<NavMeshAgent>();
        if (navmeshagent == null) Debug.LogError("NavMeshAgent not found!");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(AnimParms.IsWalking, navmeshagent.remainingDistance >= navmeshagent.stoppingDistance);
    }
}
