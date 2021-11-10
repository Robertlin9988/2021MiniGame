using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KeyDic", menuName = "InputKeys")]
public class InputKeys : ScriptableObject
{
    public static string fileName = "KeyDic";
    public static KeyCode crouchkey = KeyCode.C;

    [Header("按键事件")]
    public KeyEvents[] keyevents;
}

[Serializable]
public struct KeyEvents
{
    public KeyEventName eventname;
    public KeyCode key;
}

/// <summary>
/// 按键事件列表
/// </summary>
public enum KeyEventName
{
    /// <summary>
    /// 下蹲键
    /// </summary>
    下蹲键,
    /// <summary>
    /// 交互键
    /// </summary>
    交互键,
    /// <summary>
    /// 放置键
    /// </summary>
    放置键,
    /// <summary>
    /// 跳跃键
    /// </summary>
    跳跃键,
    /// <summary>
    /// 开火键
    /// </summary>
    开火键,
    /// <summary>
    /// 瞄准键
    /// </summary>
    瞄准键,
    /// <summary>
    /// 冲刺键
    /// </summary>
    冲刺键,
    /// <summary>
    /// 返回键
    /// </summary>
    返回键
}
