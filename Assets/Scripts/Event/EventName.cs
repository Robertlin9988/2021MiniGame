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

    //玩家开火成功事件
    public static string successshoot = "successshoot";
    //玩家子弹用完的事件
    public static string runoutofbullets = "runoutofbullets";
    //玩家拾取子弹后的事件
    public static string pickupbullets = "pickupbullets";

    //玩家受伤
    public static string playerhurt = "playerhurt";
    //敌人受伤
    public static string enemyhurt = "enemyhurt";

    public static string bullethit = "bullethit";

    /// <summary>
    /// 按键事件
    /// </summary>
    //玩家按下交互键
    public static string interactivebuttonclicked = "交互键按下";
    //玩家按下下蹲建
    public static string crouchbuttonclicked = "下蹲键按下";
    //玩家按下放置键
    public static string putitembuttonclicked = "放置键按下";
    //玩家按下跳跃键
    public static string jumpbuttonclicked = "跳跃键按下";
    //玩家开火
    public static string playeredfired = "开火键按下";
    //玩家开镜
    public static string playeraimed = "瞄准键按下";
    //玩家按下shift键
    public static string rollbuttonclicked = "冲刺键按下";
    //玩家按下esc键
    public static string escbuttonclicked = "返回键按下";

    /// <summary>
    /// 场景切换事件
    /// </summary>
    public static string sceneload = "sceneload";
    public static string sceneunload = "sceneunload";

    public static string dialogfinish = "dialogfinish";

}
