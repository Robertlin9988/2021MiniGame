using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterInfos", menuName = "CharacterInfos")]
public class CharacterInfos : ScriptableObject
{
    [Header("人物信息")]
    public NPCInfo[] npc;
}

[Serializable]
public struct NPCInfo
{
    public string name;
    public Sprite faceimg;
}
