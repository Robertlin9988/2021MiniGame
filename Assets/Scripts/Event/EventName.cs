using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 记录所有事件的名字
/// </summary>
public class EventName
{

    /// <summary>
    /// 场景事件
    /// </summary>
    //玩家丢弃物品后的事件
    public static string enemypatroldisturbance = "enemypatroldisturbance";
    public static string enemyarrivedisturbance = "enemyarrivedisturbance";

    /// <summary>
    /// 按键事件
    /// </summary>
    //玩家按下交互键
    public static string interactivebuttonclicked = "交互键按下";
    //玩家按下下蹲建
    public static string crouchbuttonclicked = "下蹲键按下";
    //玩家按下放置键
    public static string putitembuttonclicked = "放置键按下";

    /// <summary>
    /// 场景切换事件
    /// </summary>
    public static string sceneload = "sceneload";
    public static string sceneunload = "sceneunload";

    public static string dialogfinish = "dialogfinish";

}
