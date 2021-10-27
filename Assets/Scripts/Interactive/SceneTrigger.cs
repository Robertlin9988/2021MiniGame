using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SceneTrigger : Interactive
{
    public int sceneindex;
    public float changeseonds;
    public PostProcessProfile postprocessprofile;
    public float targetexposure;

    private float currenttime;
    private bool trigger;
    private float startexposure=0.5f;


    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            trigger = true;
            currenttime = 0;
            Time.timeScale = 0;//ֻӰ��fixedupdate ��Ӱ��update ��time.deltatimeΪ����
            startexposure = postprocessprofile.GetSetting<ColorGrading>().postExposure.value;
        }
    }

    private void Update()
    {
        if(trigger)
        {
            Debug.Log("loading");
            postprocessprofile.GetSetting<ColorGrading>().postExposure.value = Mathf.Lerp(startexposure,targetexposure,currenttime/changeseonds);
            if(currenttime>= changeseonds)
            {
                ScenneManagement.GetInstance().LoadSceneAdditive(sceneindex);
                Destroy(this.gameObject);
            }
            currenttime += 0.02f;
        }
    }

    private void OnDestroy()
    {
        postprocessprofile.GetSetting<ColorGrading>().postExposure.value = startexposure;
        Time.timeScale = 1;
        currenttime = 0;
        trigger = false;
    }
}
