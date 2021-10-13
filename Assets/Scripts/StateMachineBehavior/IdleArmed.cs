using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armed State", menuName = "CharacterState/Armed State")]
public class IdleArmed : StateData
{
    [Header("投掷距离")]
    [Range(1, 10)] public float range=5;

    //仅生成一次
    private static BulletTrajectory trajectory;

    public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(trajectory==null)
            trajectory = VFXManager.GetInstance().PlayandGetVFX<BulletTrajectory>(VFXName.BulletTrajectory);
    }

    public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (trajectory == null) return;
        trajectory.startpoint = animator.transform.position;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerName.terriainlayer))
        {
            //判断是否穿墙及计算终点位置
            Vector3 dir = hit.point - animator.transform.position;
            Vector3 endpoint = (dir.magnitude <= range) ? hit.point : animator.transform.position + dir.normalized * range;
            int layer = hit.transform.gameObject.layer;
            ray = new Ray(animator.transform.position + animator.transform.up, dir);
            if (Physics.Raycast(ray, out hit, range, 1 << LayerName.walllayer))
            {
                Debug.Log("hit wall!");
                layer = LayerName.walllayer;
                trajectory.endpoint = hit.point;
            }
            else
            {
                trajectory.endpoint = endpoint;
            }

            //判断层级
            if (layer == LayerName.terriainlayer)
            {
                trajectory.SetMaterial(true);
                if (Input.GetMouseButtonDown(0))
                {
                    EventCenter.GetInstance().EventTrigger<Vector3>(EventName.enemypatroldisturbance, trajectory.endpoint);
                    animator.SetBool(AnimParms.Pickup, false);
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
            trajectory.endpoint = animator.transform.position;
        }
    }
}
