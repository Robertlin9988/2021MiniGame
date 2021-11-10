using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// ���д������򲻴�����ί�еĻ��ࣨ��ʽ�滻��
/// </summary>
public class IEventInfo
{

}


/// <summary>
/// ��һ��������ί��
/// </summary>
/// <typeparam name="T">ί�к��������ķ���</typeparam>
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

/// <summary>
/// ������������ί��
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public class EventInfo<T1,T2>:IEventInfo
{
    public UnityAction<T1,T2> actions;

    public EventInfo(UnityAction<T1,T2> action)
    {
        actions += action;
    }
}

/// <summary>
/// ����������ί��
/// </summary>
public class EventInfo : IEventInfo
{
    public UnityAction actions;

    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}



/// <summary>
/// �¼����Ĺ��������¼��ļ���
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> eventdic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// ��Ӵ�һ���������¼�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">�¼�����</param>
    /// <param name="action">�¼���ί�к���</param>
    public void AddEventListener<T>(string name,UnityAction<T> action)
    {
        //��û�ж�Ӧ���¼�����
        //�е����
        if (eventdic.ContainsKey(name))
        {
            (eventdic[name] as EventInfo<T>).actions += action;
        }
        //û�е����
        else
        {
            eventdic.Add(name, new EventInfo<T>(action));
        }
    }

    /// <summary>
    /// ��Ӵ������������¼�����
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener<T1,T2>(string name, UnityAction<T1,T2> action)
    {
        //��û�ж�Ӧ���¼�����
        //�е����
        if (eventdic.ContainsKey(name))
        {
            (eventdic[name] as EventInfo<T1,T2>).actions += action;
        }
        //û�е����
        else
        {
            eventdic.Add(name, new EventInfo<T1,T2>(action));
        }
    }

    /// <summary>
    /// ��Ӳ����������¼�����
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener(string name,UnityAction action)
    {
        //��û�ж�Ӧ���¼�����
        //�е����
        if (eventdic.ContainsKey(name))
        {
            (eventdic[name] as EventInfo).actions += action;
        }
        //û�е����
        else
        {
            eventdic.Add(name, new EventInfo(action));
        }
    }

    /// <summary>
    /// �Ƴ���Ӧ��һ�������¼�����
    /// </summary>
    /// <param name="name">�¼�������</param>
    /// <param name="action">��Ӧ֮ǰ��ӵ�ί�к���</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventdic.ContainsKey(name) && (eventdic[name] as EventInfo<T>) != null)
            (eventdic[name] as EventInfo<T>).actions -= action;
        else
            Debug.LogError("RemoveEventListener<T> failed!");
    }

    /// <summary>
    /// �Ƴ���Ӧ�����������¼�����
    /// </summary>
    /// <param name="name">�¼�������</param>
    /// <param name="action">��Ӧ֮ǰ��ӵ�ί�к���</param>
    public void RemoveEventListener<T1,T2>(string name, UnityAction<T1,T2> action)
    {
        if (eventdic.ContainsKey(name) && (eventdic[name] as EventInfo<T1,T2>) != null)
            (eventdic[name] as EventInfo<T1,T2>).actions -= action;
        else
            Debug.LogError("RemoveEventListener<T1,T2> failed!");
    }

    /// <summary>
    /// �Ƴ�����Ҫ�������¼�
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void RemoveEventListener(string name, UnityAction action)
    {
        if (eventdic.ContainsKey(name) && (eventdic[name] as EventInfo) != null)
            (eventdic[name] as EventInfo).actions -= action;
        else
            Debug.LogError("RemoveEventListener failed!");
    }

    /// <summary>
    /// �¼�����
    /// </summary>
    /// <param name="name">��һ�����ֵ��¼�������</param>
    public void EventTrigger<T>(string name, T info)
    {
        //��û�ж�Ӧ���¼�����������ת���ɹ�
        if (eventdic.ContainsKey(name))
        {
            if((eventdic[name] as EventInfo<T>) != null)
            {
                if ((eventdic[name] as EventInfo<T>).actions != null)
                    (eventdic[name] as EventInfo<T>).actions.Invoke(info);
            }
            else
            {
                Debug.LogError(name+"EventTrigger<T> failed!");
            }
        }
    }

    /// <summary>
    /// �¼�������������
    /// </summary>
    /// <param name="name">��һ�����ֵ��¼�������</param>
    public void EventTrigger<T1,T2>(string name, T1 info,T2 info2)
    {
        //��û�ж�Ӧ���¼�����������ת���ɹ�
        if (eventdic.ContainsKey(name))
        {
            if ((eventdic[name] as EventInfo<T1,T2>) != null)
            {
                if ((eventdic[name] as EventInfo<T1,T2>).actions != null)
                    (eventdic[name] as EventInfo<T1,T2>).actions.Invoke(info,info2);
            }
            else
            {
                Debug.LogError(name + "EventTrigger<T1,T2> failed!");
            }
        }
    }

    /// <summary>
    /// �¼�����������Ҫ�����ģ�
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(string name)
    {
        //��û�ж�Ӧ���¼�����������ת���ɹ�
        if (eventdic.ContainsKey(name))
        {
            if((eventdic[name] as EventInfo) != null)
            {
                if ((eventdic[name] as EventInfo).actions != null)
                    (eventdic[name] as EventInfo).actions.Invoke();
            }
            else
            {
                Debug.LogError(name+"EventTrigger failed!");
            }
        }
    }

    /// <summary>
    /// ����¼�����
    /// ��Ҫ���ڳ����л�ʱ
    /// </summary>
    public void Clear()
    {
        eventdic.Clear();
    }

}
