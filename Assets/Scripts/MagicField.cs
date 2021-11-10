using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicField : MonoBehaviour
{

    public float awaketime;
    public float targetscale;
    public float damageperhit = 1;
    public Material mat;

    public Transform insidepos;
    public Transform outpos;
    Transform player;

    private string hitplayereffect = "VFXs/StunExplosion2";
    private float currentawaketime;
    private bool hit;


    bool DetectAtk()
    {
        float playerrange = Vector3.Magnitude(player.position - transform.position);
        float insiderange= Vector3.Magnitude(insidepos.position - transform.position);
        float outrange = Vector3.Magnitude(outpos.position - transform.position);
        return player.position.y<=transform.position.y && playerrange >= insiderange && playerrange <= outrange;
    }


    private void Start()
    {
        player = CharacterState.GetThirdPersonControl().transform;
    }

    private void OnEnable()
    {
        currentawaketime = 0;
        ChangeFloorColor.GetInstance().SetColor();
        ChangeFloorColor.GetInstance().ChangeShield();
    }

    private void OnDisable()
    {
        currentawaketime = 0;
        transform.localScale = Vector3.zero;
    }


    // Update is called once per frame
    void Update()
    {
        if (currentawaketime <= awaketime)
        {
            currentawaketime += Time.deltaTime;
            float scale = Mathf.Lerp(0, targetscale, currentawaketime / awaketime);
            transform.localScale = new Vector3(scale, scale, scale);
            mat.SetFloat("_BurnRadius", Vector3.Magnitude(outpos.position - transform.position));
        }
        else
        {
            PoolMgr.GetInstance().PushObj(this.gameObject.name, this.gameObject);
        }

        if (DetectAtk())
        {
            if(!hit)
            {
                PoolMgr.GetInstance().GetObj(hitplayereffect, new Vector3(player.position.x, transform.position.y,player.position.z), Quaternion.LookRotation(Vector3.up), (o) => { });
                AudioManager.GetInstance().PlaySFXAtPoint(AudiosName.playerhurt, transform.position);
                EventCenter.GetInstance().EventTrigger<float>(EventName.playerhurt, damageperhit);
                hit = true;
            }
        }
        else
        {
            hit = false;
        }
    }
}
