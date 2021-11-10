using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResManager : BaseManager<ResManager>
{
    public T Load<T>(string name) where T:Object
    {
        //���������һ��GameObject���͵� �Ұ���ʵ������ �ٷ��س�ȥ �ⲿ ֱ��ʹ�ü���
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    public T Load<T>(string name, Vector3 pos,Quaternion rotation) where T : Object
    {
        //���������һ��GameObject���͵� �Ұ���ʵ������ �ٷ��س�ȥ �ⲿ ֱ��ʹ�ü���
        T res = Resources.Load<T>(name);
        if (res is GameObject)
            return GameObject.Instantiate(res,pos,rotation);
        else
            return res;
    }
}
