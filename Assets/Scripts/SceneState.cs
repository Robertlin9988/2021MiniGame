using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �����ڲ����ٳ����ĸ�������
/// �����������״̬�Ľű� ͨ��Onenable �� Ondisable �����������ʱִ�еĲ���
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
    /// ��Ӽ��غ�ж�س������¼�
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
