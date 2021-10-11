using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemypatrol : MonoBehaviour
{
    [Header("Ѳ��·����")]
    public Transform[] waypoints;

    [Header("���䷶Χ")]
    public float alertrange;

    [Header("ͣ��ʱ��")]
    public float waypointwaittime;
    public float disturbwaittime;
    //�Ƿ����õ����ʱ
    public bool enablearrivewait;

    
    NavMeshAgent navmeshagent;
    bool isdisturbed;
    int currentindex;
    float currentwaypointwaittime;
    float currentdisturbwaittime;

    /// <summary>
    /// ���ø��ŵ�
    /// </summary>
    /// <param name="targetpoint">���ŵ�λ������</param>
    public void SetDisturbance(Vector3 targetpoint)
    {
        //�ж��Ƿ��ھ���뾶��
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
        //����Ŷ��¼�����
        EventCenter.GetInstance().AddEventListener<Vector3>(EventName.enemypatroldisturbance, SetDisturbance);
    }

    // Update is called once per frame
    void Update()
    {
        //������
        if (isdisturbed)
        {
            if(currentdisturbwaittime>=disturbwaittime)
            {
                isdisturbed = false;
                navmeshagent.SetDestination(waypoints[currentindex].position);
            }

            //��ʱ��ʽ�Ƿ񵽵��ʱ
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

        //������·��Ѳ��
        if (!isdisturbed && navmeshagent.remainingDistance < navmeshagent.stoppingDistance)
        {
            if(currentwaypointwaittime>=waypointwaittime)
            {
                //currentindex = (currentindex + 1) % waypoints.Length;//��˳��ѭ��
                currentindex = Random.Range(0, waypoints.Length);//���ѡ��
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

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener<Vector3>(EventName.enemypatroldisturbance, SetDisturbance);
    }
}
