using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public sealed class SceneTool
{
  private static SceneTool _instance = null;
  private static readonly object SynObject = new object();
  SceneTool()
  {
  }

  public static SceneTool Instance
  {
    get
    {
      lock (SynObject)
      {
        return _instance ?? (_instance = new SceneTool());
      }
    }
  }

    public void ReloadScene(){
       Scene tmp = SceneManager.GetActiveScene();
       ChangeScene(tmp);
    }
    public void ChangeScene(Scene s){
        SceneManager.LoadScene(s.name);
    }


}
