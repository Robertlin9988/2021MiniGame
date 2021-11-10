using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 统一管理UI Panel的显示与隐藏
/// </summary>
public class UIManager : BaseManager<UIManager>
{
    //记录所有当前已经显示的Panel
    public Dictionary<string, BasePanel> paneldic = new Dictionary<string, BasePanel>();

    //记录我们UI的Canvas父对象 方便以后外部可能会使用它
    public RectTransform canvas;

    public UIManager()
    {
        GameObject canvasobj = GameObject.Find(PanelName.canvas);
        if (canvasobj == null)
        {
            Debug.LogError("Canvas not exist!");
            return;
        }
        canvas = canvasobj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(canvasobj);

        GameObject eventsysobj = GameObject.Find(PanelName.eventsys);
        if (eventsysobj == null)
        {
            Debug.LogError("EventSystem not exist!");
            return;
        }
        GameObject.DontDestroyOnLoad(eventsysobj);
    }


    public void ShowPanel<T>(string panelname, UnityAction<T> callBack = null) where T:BasePanel
    {
        //已经显示
        if(paneldic.ContainsKey(panelname))
        {
            if (callBack != null)
            {
                callBack(paneldic[panelname] as T);
            }
            paneldic[panelname].OnPanelShow();
            return;
        }

        Transform paneltransform = canvas.transform.Find(panelname);
        if(paneltransform == null)
        {
            Debug.LogError(panelname + " not exist!");
            return;
        }

        T panel = paneltransform.GetComponent<T>();
        if(panel==null)
        {
            Debug.LogError("Panel script not exist!");
            return;
        }

        paneltransform.gameObject.SetActive(true);

        if (callBack!=null)
        {
            callBack(panel);
        }

        panel.OnPanelShow();

        paneldic.Add(panelname, panel);

    }

    public void HidePanel(string panelname)
    {
        if(paneldic.ContainsKey(panelname)&& paneldic[panelname]!=null)
        {
            paneldic[panelname].OnPanelHide();
            paneldic[panelname].gameObject.SetActive(false);
            paneldic.Remove(panelname);
        }
    }

    public void HideALLPanel()
    {
        //未被清除的情况下
        if(canvas!=null)
        {
            for (int i = 0; i < canvas.childCount; i++)
            {
                paneldic.Clear();
                canvas.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public T GetPanel<T>(string name) where T:BasePanel
    {
        if (paneldic.ContainsKey(name))
            return paneldic[name] as T;
        return null;
    }
}
