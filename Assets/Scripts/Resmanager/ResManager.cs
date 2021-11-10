using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : BaseManager<ResManager>
{
    public T Load<T>(string name) where T:Object
    {
        //如果对象是一个GameObject类型的 我把他实例化后 再返回出去 外部 直接使用即可
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    public T Load<T>(string name, Vector3 pos,Quaternion rotation) where T : Object
    {
        //如果对象是一个GameObject类型的 我把他实例化后 再返回出去 外部 直接使用即可
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res,pos,rotation);
        else
            return res;
    }
}
