using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


/// <summary>
/// 面板基类
/// 可直接获取面板上的所有组件
/// 可直接对组件添加监听事件
/// 通过UIManager调用Panel
/// </summary>
public class BasePanel : MonoBehaviour
{
    //普通UI组件
    Dictionary<string, List<UIBehaviour>> uicomponents = new Dictionary<string, List<UIBehaviour>>();
    //TMPro组件
    Dictionary<string, TextMeshProUGUI> tmprocomponents = new Dictionary<string, TextMeshProUGUI>();

    protected virtual void Awake()
    {
        SetChidrenComponents<Button>();
        SetChidrenComponents<Image>();
        SetChidrenComponents<Text>();
        SetChidrenComponents<GridLayoutGroup>();
        SetTMProcomponent();
    }



    //按钮点击事件虚函数由子类实现
    protected virtual void OnClick(string name)
    {

    }

    //面板显示时执行的初始化操作
    public virtual void OnPanelShow()
    {

    }

    //面板隐藏时执行的初始化操作
    public virtual void OnPanelHide()
    {

    }


    public T GetComponent<T>(string name) where T:UIBehaviour
    {
        if(uicomponents.ContainsKey(name))
        {
            foreach(UIBehaviour component in uicomponents[name])
            {
                if(component is T)
                {
                    return component as T;
                }
            }
        }
        return null;
    }


    public TextMeshProUGUI GetTMPro(string name)
    {
        if(tmprocomponents.ContainsKey(name))
        {
            return tmprocomponents[name];
        }
        return null;
    }




    private void SetChidrenComponents<T>() where T:UIBehaviour
    {
        //获取子物体中所有T型组件
        T[] components = GetComponentsInChildren<T>();
        foreach(T component in components)
        {
            string name = component.gameObject.name;
            if(uicomponents.ContainsKey(name))
            {
                uicomponents[name].Add(component);
            }
            else
            {
                uicomponents.Add(name, new List<UIBehaviour>() { component });
            }

            //如果是按钮控件则添加点击监听
            if (component is Button)
            {
                (component as Button).onClick.AddListener(() =>
                {
                    OnClick(name);
                });
            }
        }
    }

    private void SetTMProcomponent()
    {
        TextMeshProUGUI[] components = this.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (TextMeshProUGUI component in components)
        {
            string name = component.gameObject.name;
            if (!tmprocomponents.ContainsKey(name))
                tmprocomponents.Add(name, component);
        }
    }
}
