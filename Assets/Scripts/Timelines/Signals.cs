using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Signals : MonoBehaviour
{
    public Animator anim;

    public void settrans()
    {
        anim.SetTrigger(AnimParam.statetrans);
    }


}
