using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// ����������
/// </summary>
public class ScenneManagement : MonoBehaviour
{
    private static ScenneManagement instance;
    private static GameObject scenemanager;
    private static string objname = "SceneManager";


    public static ScenneManagement GetInstance()
    {
        if (scenemanager == null)
        {
            scenemanager = GameObject.Find(objname);
            if (scenemanager == null)
            {
                scenemanager = new GameObject();
                scenemanager.name = objname;
            }
        }
        if (instance == null)
        {
            instance = scenemanager.GetComponent<ScenneManagement>();
            if (instance == null)
            {
                instance = scenemanager.AddComponent<ScenneManagement>();
            }
        }
        return instance;
    }

    public void LoadSceneAdditive(int index)
    {
        EventCenter.GetInstance().EventTrigger(EventName.sceneload);
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }

    public void LoadSceneSingle(int index)
    {
        OnSceneLoad();
        SceneManager.LoadScene(index);
    }

    /// <summary>
    /// ֻ�е�ADDDITIVEʱʹ��
    /// </summary>
    /// <param name="index"></param>
    public void UnloadScene(int index)
    {
        EventCenter.GetInstance().EventTrigger(EventName.sceneunload);
        SceneManager.UnloadSceneAsync(index);
    }


    /// <summary>
    /// Called on Awake ͳһ����������ĳ�ʼ������ �������������Start��ִ�г�ʼ������
    /// </summary>
    public void InitialSceneSettings()
    {

    }

    /// <summary>
    /// �л�����ʱִ�е�һЩ�в���
    /// </summary>
    private void OnSceneLoad()
    {
        AudioManager.GetInstance().StopAll();
        PoolMgr.GetInstance().Clear();
        EventCenter.GetInstance().Clear();
        UIManager.GetInstance().HideALLPanel();
    }
}
