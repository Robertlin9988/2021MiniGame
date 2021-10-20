using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : BaseManagerMono<TimelineManager>
{

    private PlayableDirector currentdirector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



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
}
