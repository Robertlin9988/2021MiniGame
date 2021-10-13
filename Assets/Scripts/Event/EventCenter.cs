using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// 所有带参数或不带参数委托的基类（里式替换）
/// </summary>
public class IEventInfo
{

}


/// <summary>
/// 带一个参数的委托
/// </summary>
/// <typeparam name="T">委托函数参数的泛型</typeparam>
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;

    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

/// <summary>
/// 不带参数的委托
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
/// 事件中心管理所有事件的监听
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
    private Dictionary<string, IEventInfo> eventdic = new Dictionary<string, IEventInfo>();

    /// <summary>
    /// 添加带一个参数的事件监听
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name">事件名称</param>
    /// <param name="action">事件的委托函数</param>
    public void AddEventListener<T>(string name,UnityAction<T> action)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventdic.ContainsKey(name))
        {
            (eventdic[name] as EventInfo<T>).actions += action;
        }
        //没有的情况
        else
        {
            eventdic.Add(name, new EventInfo<T>(action));
        }
    }

    /// <summary>
    /// 添加不带参数的事件监听
    /// </summary>
    /// <param name="name"></param>
    /// <param name="action"></param>
    public void AddEventListener(string name,UnityAction action)
    {
        //有没有对应的事件监听
        //有的情况
        if (eventdic.ContainsKey(name))
        {
            (eventdic[name] as EventInfo).actions += action;
        }
        //没有的情况
        else
        {
            eventdic.Add(name, new EventInfo(action));
        }
    }

    /// <summary>
    /// 移除对应的事件监听
    /// </summary>
    /// <param name="name">事件的名字</param>
    /// <param name="action">对应之前添加的委托函数</param>
    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if (eventdic.ContainsKey(name) && (eventdic[name] as EventInfo<T>) != null)
            (eventdic[name] as EventInfo<T>).actions -= action;
        else
            Debug.LogError("RemoveEventListener<T> failed!");
    }

    /// <summary>
    /// 移除不需要参数的事件
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
    /// 事件触发
    /// </summary>
    /// <param name="name">哪一个名字的事件触发了</param>
    public void EventTrigger<T>(string name, T info)
    {
        //有没有对应的事件监听且类型转换成功
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
    /// 事件触发（不需要参数的）
    /// </summary>
    /// <param name="name"></param>
    public void EventTrigger(string name)
    {
        //有没有对应的事件监听且类型转换成功
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
    /// 清空事件中心
    /// 主要用在场景切换时
    /// </summary>
    public void Clear()
    {
        eventdic.Clear();
    }

}
