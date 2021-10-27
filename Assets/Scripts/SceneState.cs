using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �����������״̬�Ľű� ͨ��Onenable �� Ondisable �����������ʱִ�еĲ���
/// </summary>
public class SceneState : MonoBehaviour
{
    /// <summary>
    /// ��Ӽ��غ�ж�س������¼�
    /// </summary>
    private void OnEnable()
    {
        EventCenter.GetInstance().AddEventListener(EventName.sceneload, () =>
        {
            this.gameObject.SetActive(false);
        });
        EventCenter.GetInstance().AddEventListener(EventName.sceneunload, () =>
        {
            this.gameObject.SetActive(true);
        });
        OperaManager.GetInstance().SetStateTrans();
        Debug.Log("enter");
    }

    private void OnDisable()
    {
        Debug.Log("exit");
    }
}
