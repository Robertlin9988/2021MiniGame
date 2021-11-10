using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Particles", menuName = "ParticleInfos")]
public class Particles : ScriptableObject
{
    public ParticleInfo[] PlayerSFX;
}

[Serializable] 
public struct ParticleInfo
{
    public string name;
    public string particlefxfile;
}
