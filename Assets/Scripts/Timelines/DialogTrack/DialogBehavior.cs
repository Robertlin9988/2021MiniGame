using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


[Serializable]
public class DialogBehavior : PlayableBehaviour
{
    [Header("人物名字")]
    public string charactername;
    [Header("对话内容")]
    [TextArea(8, 1)] public string dialoguecontent;
    [Header("文字大小")]
    public int textsize;
    [Header("是否需要玩家确认")]
    public bool pauseafterdialog;

    /// <summary>
    /// 当前片段是否正在播放
    /// </summary>
    private bool isclipplay;
    /// <summary>
    /// 是否暂停
    /// </summary>
    private bool ispauseschedule;
    /// <summary>
    /// 当前的pd组件
    /// </summary>
    private PlayableDirector director;


    void SetDialogueInfo(SentencePanel o)
    {
        o.GetComponent<Text>(PanelName.content).text = dialoguecontent;
        o.GetComponent<Text>(PanelName.content).fontSize = textsize;
    }


    /// <summary>
    /// 该TimeLine Awake播放时、从头Play时
    /// </summary>
    /// <param name="playable"></param>
    public override void OnPlayableCreate(Playable playable)
    {
        director = playable.GetGraph().GetResolver() as PlayableDirector;
    }    

    /// <summary>
    /// 当时间轴在该代码片段时，每帧执行
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //若还未播放该clip，当前clip的权重>0
        //效果是时间轴刚进入该clip时执行一次
        if (isclipplay==false && info.weight>0)
        {
            if (pauseafterdialog)
                ispauseschedule = true;
            isclipplay = true;
            UIManager.GetInstance().ShowPanel<SentencePanel>(PanelName.sentencepanel, SetDialogueInfo);
        }
    }


    /// <summary>
    /// 当时间轴在该代码区域：Pause、Stop时执行一次
    ///当从头播放该TimeLine时执行一次
    ///当时间轴驶出该代码区域时执行一次
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (isclipplay) UIManager.GetInstance().HidePanel(PanelName.sentencepanel);
        isclipplay = false;
        Debug.Log("Pause");
        if(ispauseschedule)
        {
            ispauseschedule = false;
            TimelineManager.GetInstance().PauseTimeLine(director);
        }
    }
}
