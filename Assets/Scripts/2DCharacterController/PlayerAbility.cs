using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("投掷距离")]
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
            //从光心到指定像素方向上的一条射线(起点为近裁平面位置)
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
