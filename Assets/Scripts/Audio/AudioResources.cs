using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AudioResources", menuName = "AudioResources")]
public class AudioResources : ScriptableObject
{
    public AudioInfo[] BGM;
    public AudioInfo[] SFX;
}

[Serializable]
public struct AudioInfo
{
    public string name;
    public AudioClip clip;
}
