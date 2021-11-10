using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHP : MonoBehaviour
{

    protected Animator anim;
    protected float currenthp;
    public float fullhp;


    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        currenthp = fullhp;
        //������д�����඼�ᱻ����
        //EventCenter.GetInstance().AddEventListener<float>(EventName.playerhurt, Gethurt);
    }

    protected virtual void Gethurt(float value)
    {
        //Debug.Log("Base");
        currenthp -= value;
        if(anim!=null)
            anim.SetTrigger(AnimParms.gethurt);
        if(currenthp<=0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {

    }
}
