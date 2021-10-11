using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 人物动画状态机的一些条件参数
/// </summary>
public class AnimParms
{
    //是否移动的参数
    public static string IsWalking = "IsWalking";

    //门开关触发器
    public static string triggerdoor = "triggerdoor";
}




public class AnimatorController : MonoBehaviour
{
    protected Animator anim;

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("animcontroller not found!");
    }
}
