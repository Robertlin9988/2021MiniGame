using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemypatrol : MonoBehaviour
{
    [Header("Ñ²ÂßÂ·¾¶µã")]
    public Transform[] waypoints;

    NavMeshAgent navmeshagent;
    int currentindex;
    

    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        if (navmeshagent == null) Debug.LogError("NavMeshAgent not found!");
        if (waypoints.Length == 0) Debug.LogError("Way points not set!");
        navmeshagent.SetDestination(waypoints[currentindex].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (navmeshagent.remainingDistance < navmeshagent.stoppingDistance)
        {
            currentindex = (currentindex + 1) % waypoints.Length;
            navmeshagent.SetDestination(waypoints[currentindex].position);
        }
    }
}
