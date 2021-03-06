using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 人物动画状态机的一些条件参数
/// </summary>
public class AnimParms
{
    /// <summary>
    /// 是否移动的参数
    /// </summary>
    public static string IsWalking = "IsWalking";

    /// <summary>
    /// 门开关触发器
    /// </summary>
    public static string triggerdoor = "triggerdoor";

    /// <summary>
    /// 蹲下状态
    /// </summary>
    public static string IsCouching = "IsCouching";

    /// <summary>
    /// 拾取与放下东西
    /// </summary>
    public static string Pickup = "Armed";

    /// <summary>
    /// 跳跃
    /// </summary>
    public static string Jump = "Jump";
    public static string Land = "Land";
    public static string Roll = "roll";


    public static string horizontal = "horizontal";
    public static string vertical = "vertical";

    public static string aiming = "Armed";
    public static string fire = "Shoot";

    public static string gethurt = "GetHurt";
    public static string resume = "Resume";
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
