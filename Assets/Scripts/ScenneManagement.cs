using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 场景管理单例
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
    /// 只有当ADDDITIVE时使用
    /// </summary>
    /// <param name="index"></param>
    public void UnloadScene(int index)
    {
        EventCenter.GetInstance().EventTrigger(EventName.sceneunload);
        SceneManager.UnloadSceneAsync(index);
    }


    /// <summary>
    /// Called on Awake 统一管理场景物体的初始化操作 被管理的物体在Start中执行初始化操作
    /// </summary>
    public void InitialSceneSettings()
    {

    }

    /// <summary>
    /// 切换场景时执行的一些列操作
    /// </summary>
    private void OnSceneLoad()
    {
        AudioManager.GetInstance().StopAll();
        PoolMgr.GetInstance().Clear();
        EventCenter.GetInstance().Clear();
        UIManager.GetInstance().HideALLPanel();
    }
}
