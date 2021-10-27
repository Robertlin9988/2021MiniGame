using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public enum timelinename
{
    MirrorBreak1,
    MirrorBreak2,
    WakeUp
}

[Serializable]
public struct Timelineassets
{
    public timelinename name;
    public PlayableDirector director;
}


public class TimelineManager : MonoBehaviour
{
    private PlayableDirector currentdirector;
    private static TimelineManager instance;
    private Dictionary<string, PlayableDirector> directordic=new Dictionary<string, PlayableDirector>();
    public static TimelineManager GetInstance() => instance;
    public Timelineassets[] directorofthisscene;

    //每个场景只有只一个实例的单例 场景重新加载会重置
    private void Awake()
    {
        instance=this;
        foreach(Timelineassets director in directorofthisscene)
        {
            directordic.Add(director.name.ToString(), director.director);
        }
    }




    public void PauseTimeScale()
    {
        Time.timeScale = 0;
    }

    public void ResumeTimeScale()
    {
        Time.timeScale = 1;
    }


    public void PlayTimeline(PlayableDirector director)
    {
        currentdirector = director;
        currentdirector.Play();
    }

    public void PlayTimeline(string name)
    {
        if(directordic.ContainsKey(name))
        {
            currentdirector = directordic[name];
            currentdirector.Play();
        }
    }


    public void PauseTimeLine()
    {
        currentdirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }

    /// <summary>
    /// playonawake 的timeline需要传入director初始化currentdirector
    /// </summary>
    /// <param name="director"></param>
    public void PauseTimeLine(PlayableDirector director)
    {
        currentdirector = director;
        //Pause方法会直接暂停导致将不再update
        //currentdirector.Pause();
        currentdirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
    }

    public void ResumeTimeLine()
    {
        //currentdirector.Resume();
        currentdirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
    }

    public void StopTimeLine()
    {
        currentdirector.Stop();
    }
}
