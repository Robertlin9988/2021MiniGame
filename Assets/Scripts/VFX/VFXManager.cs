using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������Ч������
/// </summary>
public class VFXManager : BaseManager<VFXManager>
{
    /// <summary>
    /// ��¼������Чĸ������ʵ����
    /// </summary>
    private Dictionary<string, BaseVFX> vfxdic = new Dictionary<string, BaseVFX>();

    public VFXManager()
    {
        VFX vfxobject = Resources.Load(VFXName.vfxobject) as VFX;
        if (vfxobject == null) Debug.LogError("vfxobject not found in resources!");
        foreach (VFXInfo info in vfxobject.vfxresources)
        {
            vfxdic.Add(info.name, info.vfx);
        }
    }

    /// <summary>
    /// ʵ������Ч������ʵ�����Ķ���
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T PlayandGetVFX<T>(string name) where T : BaseVFX
    {
        GameObject obj=GameObject.Instantiate(vfxdic[name].gameObject);
        T component = obj.GetComponent<T>();
        return component;
    }
}
