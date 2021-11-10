using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KeyDic", menuName = "InputKeys")]
public class InputKeys : ScriptableObject
{
    public static string fileName = "KeyDic";
    public static KeyCode crouchkey = KeyCode.C;

    [Header("�����¼�")]
    public KeyEvents[] keyevents;
}

[Serializable]
public struct KeyEvents
{
    public KeyEventName eventname;
    public KeyCode key;
}

/// <summary>
/// �����¼��б�
/// </summary>
public enum KeyEventName
{
    /// <summary>
    /// �¶׼�
    /// </summary>
    �¶׼�,
    /// <summary>
    /// ������
    /// </summary>
    ������,
    /// <summary>
    /// ���ü�
    /// </summary>
    ���ü�,
    /// <summary>
    /// ��Ծ��
    /// </summary>
    ��Ծ��,
    /// <summary>
    /// �����
    /// </summary>
    �����,
    /// <summary>
    /// ��׼��
    /// </summary>
    ��׼��,
    /// <summary>
    /// ��̼�
    /// </summary>
    ��̼�,
    /// <summary>
    /// ���ؼ�
    /// </summary>
    ���ؼ�
}
