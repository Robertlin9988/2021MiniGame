using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("Í¶ÖÀ¾àÀë")]
    [Range(1,10)] public float range;

    private bool armed;
    private BulletTrajectory trajectory;


    public void SetArmed(GameObject obj)
    {
        trajectory=VFXManager.GetInstance().PlayandGetVFX<BulletTrajectory>(VFXName.BulletTrajectory);
        armed = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //´ý¸Ä½ø
        if (armed)
        {
            trajectory.startpoint = transform.position;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit,Mathf.Infinity,1<<LayerName.terriainlayer))
            {
                //ÅÐ¶ÏÊÇ·ñ´©Ç½¼°¼ÆËãÖÕµãÎ»ÖÃ
                Vector3 dir = hit.point - transform.position;
                Vector3 endpoint= (dir.magnitude <= range) ? hit.point : transform.position + dir.normalized * range;
                int layer = hit.transform.gameObject.layer;
                ray = new Ray(transform.position+transform.up, dir);
                if(Physics.Raycast(ray,out hit,range,1<<LayerName.walllayer))
                {
                    Debug.Log("hit wall!");
                    layer = LayerName.walllayer;
                    trajectory.endpoint = hit.point;
                }
                else
                {
                    trajectory.endpoint = endpoint;
                }

                //ÅÐ¶Ï²ã¼¶
                if (layer == LayerName.terriainlayer)
                {
                    trajectory.SetMaterial(true);
                    if (Input.GetMouseButtonDown(0))
                    {
                        EventCenter.GetInstance().EventTrigger<Vector3>(EventName.enemypatroldisturbance, trajectory.endpoint);
                        armed = false;
                        Destroy(trajectory.gameObject);
                        trajectory = null;
                    }
                }
                else
                {
                    trajectory.SetMaterial(false);
                }
            }
            else
            {
                trajectory.endpoint = transform.position;
            }
        }
    }
}
