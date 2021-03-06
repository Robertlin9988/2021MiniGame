using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemypatrol : MonoBehaviour
{
    [Header("巡逻路径点")]
    public Transform[] waypoints;

    [Header("警戒范围")]
    public float alertrange;

    [Header("停滞时间")]
    public float waypointwaittime;
    public float disturbwaittime;
    //是否启用到点计时
    public bool enablearrivewait;

    [Header("超时时间")]
    public float maxwaittime;

    
    NavMeshAgent navmeshagent;
    bool isdisturbed;
    int currentindex;
    float currentwaypointwaittime;
    float currentdisturbwaittime;
    float currentwaittime;

    /// <summary>
    /// 重置干扰点
    /// </summary>
    /// <param name="targetpoint">干扰点位置坐标</param>
    public void SetDisturbance(Vector3 targetpoint)
    {
        Debug.Log(targetpoint);
        //判断是否处于警戒半径内
        float dis = Vector3.Distance(transform.position, targetpoint);
        if(dis<=alertrange)
        {
            currentdisturbwaittime = 0;
            isdisturbed = true;
            navmeshagent.SetDestination(targetpoint);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        navmeshagent = GetComponent<NavMeshAgent>();
        if (navmeshagent == null) Debug.LogError("NavMeshAgent not found!");
        if (waypoints.Length == 0) Debug.LogError("Way points not set!");
        navmeshagent.SetDestination(waypoints[currentindex].position);
        //添加扰动事件监听
        EventCenter.GetInstance().AddEventListener<Vector3>(EventName.enemypatroldisturbance, SetDisturbance);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentwaittime>=maxwaittime)
        {
            isdisturbed = false;
            currentdisturbwaittime = 0;
            currentwaypointwaittime = 0;
            currentwaittime = 0;
            currentindex = Random.Range(0, waypoints.Length);//随机选择
            navmeshagent.SetDestination(waypoints[currentindex].position);
        }
        currentwaittime += Time.deltaTime;

        //被干扰
        if (isdisturbed)
        {
            if(currentdisturbwaittime>=disturbwaittime)
            {
                Debug.Log("arrive disturbance");
                currentwaittime = 0;
                isdisturbed = false;
                EventCenter.GetInstance().EventTrigger(EventName.enemyarrivedisturbance);
                navmeshagent.SetDestination(waypoints[currentindex].position);
            }

            //计时方式是否到点计时
            if(enablearrivewait)
            {
                if(navmeshagent.remainingDistance < navmeshagent.stoppingDistance)
                {
                    currentdisturbwaittime += Time.deltaTime;
                }
            }
            else
            {
                currentdisturbwaittime += Time.deltaTime;
            }
        }

        //按正常路径巡逻
        if (!isdisturbed && navmeshagent.remainingDistance < navmeshagent.stoppingDistance)
        {
            if(currentwaypointwaittime>=waypointwaittime)
            {
                currentwaittime = 0;
                //currentindex = (currentindex + 1) % waypoints.Length;//按顺序循环
                currentindex = Random.Range(0, waypoints.Length);//随机选择
                navmeshagent.SetDestination(waypoints[currentindex].position);
                currentwaypointwaittime = 0;
            }
            currentwaypointwaittime += Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, alertrange);
    }
}
