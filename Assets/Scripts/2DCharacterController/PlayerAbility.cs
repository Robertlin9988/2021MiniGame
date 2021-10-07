using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("Ͷ������")]
    [Range(0,2)] public float range;

    public bool armed { get; set; }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(armed&&Input.GetMouseButtonDown(0))
        {
            //�ӹ��ĵ�ָ�����ط����ϵ�һ������(���Ϊ����ƽ��λ��)
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                EventCenter.GetInstance().EventTrigger<Vector3>(EventName.enemypatroldisturbance, hit.point);
                armed = false;
            }
        }
    }
}
