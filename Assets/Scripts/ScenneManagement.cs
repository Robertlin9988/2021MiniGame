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
    public static ScenneManagement GetInstance() => instance;


    private void Awake()
    {
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    public void LoadSceneAdditive(int index)
    {
        EventCenter.GetInstance().EventTrigger(EventName.sceneload);
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }

    public void LoadSceneSingle(int index)
    {
        SceneManager.LoadScene(index);
    }


    public void UnloadScene(int index)
    {
        EventCenter.GetInstance().EventTrigger(EventName.sceneunload);
        SceneManager.UnloadSceneAsync(index);
    }
}
