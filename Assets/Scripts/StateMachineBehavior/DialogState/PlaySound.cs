using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct SoundsInfo
{
    public AudiosName.audioname soundname;
    public float volume;
    public bool isloop;
    public bool stoponexit;
}


public class PlaySound : StateMachineBehaviour
{
    public SoundsInfo[] soundname;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        foreach(var info in soundname)
        {
            AudioManager.GetInstance().PlaySFX(info.soundname.ToString(), info.isloop, info.volume);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        foreach (var info in soundname)
        {
            if(info.stoponexit)
                AudioManager.GetInstance().StopSound(info.soundname.ToString());
        }
    }
}
