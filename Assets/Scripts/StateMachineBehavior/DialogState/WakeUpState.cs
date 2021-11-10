using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


[Serializable]
public struct openeyeeffect
{
    public float timestep;
    [Range(0,1)]
    public float vignetteintensity;
    [Range(1, 300)]
    public float Dizzleintensity;
}


public class WakeUpState : StateMachineBehaviour
{

    public PostProcessProfile postprocessprofile;
    [Header("苏醒时间参数")]
    public openeyeeffect[] vignetteparms;
    public float darktime;

    private float currenttime;
    private int curindex;
    private openeyeeffect lastparm;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        currenttime = 0;
        curindex = 0;
        lastparm.timestep = darktime;
        lastparm.vignetteintensity = 1;
        lastparm.Dizzleintensity = 300;
        postprocessprofile.GetSetting<Vignette>().center.value = new Vector2(-1, -1);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        currenttime += Time.deltaTime;
        if (currenttime > darktime)
        {
            postprocessprofile.GetSetting<Vignette>().center.value = new Vector2(0.5f, 0.5f);
        }
        if (curindex >= vignetteparms.Length)
        {
            return;
        }
        float targettime = vignetteparms[curindex].timestep + darktime;
        float targetintensity = vignetteparms[curindex].vignetteintensity;
        float targetdizzle = vignetteparms[curindex].Dizzleintensity;
        float curvalue = postprocessprofile.GetSetting<Vignette>().intensity.value;
        float curdizzle = postprocessprofile.GetSetting<DepthOfField>().focalLength.value;
        postprocessprofile.GetSetting<Vignette>().intensity.value = Mathf.Lerp(lastparm.vignetteintensity, targetintensity, (currenttime - lastparm.timestep) / (targettime - lastparm.timestep));
        postprocessprofile.GetSetting<DepthOfField>().focalLength.value = Mathf.Lerp(lastparm.Dizzleintensity, targetdizzle, (currenttime - lastparm.timestep) / (targettime - lastparm.timestep));
        if (currenttime > targettime)
        {
            lastparm.vignetteintensity = targetintensity;
            lastparm.timestep = targettime;
            lastparm.Dizzleintensity = targetdizzle;
            curindex++;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {

    }

}
