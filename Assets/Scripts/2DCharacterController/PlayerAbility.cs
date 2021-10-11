using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("Í¶ÖÀ¾àÀë")]
    [Range(1,10)] public float range;

    private bool armed;
    private BulletTrajectory trajectory;


    public void SetArmed()
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
        if(armed)
        {
            trajectory.startpoint = transform.position;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                Vector3 dir = hit.point - transform.position;
                trajectory.endpoint = (dir.magnitude <= range) ? hit.point : transform.position + dir.normalized * range;
                if (hit.transform.gameObject.layer==LayerName.terriainlayer)
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
