using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : CharacterHP
{
    protected override void Gethurt(float value)
    {
        base.Gethurt(value);
        //Debug.Log("child1");
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log("playerdie!");
        anim.SetTrigger("Die");
    }


    protected override void Start()
    {
        base.Start();
        EventCenter.GetInstance().AddEventListener<float>(EventName.playerhurt, Gethurt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
