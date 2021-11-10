using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 挂载在不销毁场景的根物体上
/// 管理基础场景状态的脚本 通过Onenable 和 Ondisable 管理进出场景时执行的操作
/// </summary>
public class SceneState : MonoBehaviour
{

    private void Awake()
    {
        EventCenter.GetInstance().AddEventListener(EventName.sceneload, () =>
        {
            this.gameObject.SetActive(false);
        });
        EventCenter.GetInstance().AddEventListener(EventName.sceneunload, () =>
        {
            this.gameObject.SetActive(true);
        });
    }


    /// <summary>
    /// 添加加载和卸载场景的事件
    /// </summary>
    private void OnEnable()
    {
        OperaManager.GetInstance().SetStateTrans();
        Debug.Log("enter");
    }

    private void OnDisable()
    {
        Debug.Log("exit");
    }
}
