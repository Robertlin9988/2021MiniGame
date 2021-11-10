using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : CharacterHP
{

    protected override void Gethurt(float value)
    {
        base.Gethurt(value);
        EventCenter.GetInstance().EventTrigger<float>(EventName.enemyhurt, value);
        //Debug.Log("child2");
    }

    void DamageDetect(float value,int colorindex)
    {
        if(colorindex==ChangeFloorColor.GetInstance().colorindex)
        {
            Gethurt(value);
        }
    }


    protected override void Die()
    {
        base.Die();
        Debug.Log("enemy die");
        PlayerPrefs.SetString(savesettings.operastatename, "ThirdSceneFinish");
        PlayerPrefs.SetInt(savesettings.birthpoint, 4);
        PlayerPrefs.SetInt(savesettings.mirrorstate, 1);
        TimelineManager.GetInstance().PlayTimeline(timelinename.Win.ToString());
    }

    protected override void Start()
    {
        base.Start();
        EventCenter.GetInstance().AddEventListener<float,int>(EventName.bullethit, DamageDetect);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
