using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̳�monobehavior�ĵ�����������ڳ��������� �޷�newʵ����
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseManagerMono<T> : MonoBehaviour where T: MonoBehaviour
{
    private static string BaseManagerMonoName = "MonoSingletonRoot";
    private static GameObject MonoSingletonRoot;
    private static T instance;
    public static T GetInstance()
    {
        if(MonoSingletonRoot==null)
        {
            MonoSingletonRoot = GameObject.Find(BaseManagerMonoName);
            if(MonoSingletonRoot==null)
            {
                MonoSingletonRoot= new GameObject();
                MonoSingletonRoot.name = BaseManagerMonoName;
                DontDestroyOnLoad(MonoSingletonRoot);
            }
        }
        if(instance==null)
        {
            instance = MonoSingletonRoot.GetComponent<T>();
            if(instance==null)
            {
                instance = MonoSingletonRoot.AddComponent<T>();
            }
        }
        return instance;
    }
}
