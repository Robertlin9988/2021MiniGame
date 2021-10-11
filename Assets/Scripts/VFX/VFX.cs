using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VFXs", menuName = "VFXInfos")]
public class VFX : ScriptableObject
{
    [Header("��Ч��Դ")]
    public VFXInfo[] vfxresources;
}

[Serializable]
public struct VFXInfo
{
    public string name;
    public BaseVFX vfx;
}
