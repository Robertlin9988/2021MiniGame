using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


[Serializable]
public class DialogBehaviorTxt : PlayableBehaviour
{
    [Header("对话内容txt文本")]
    public TextAsset dialogs;

    //当前段落行数
    int currentindex;
    //存储段落的一行行文本
    List<string> sentences = new List<string>();
    //人物名字对应的头像
    static CharacterInfos characterinfos;
    static Dictionary<string, Sprite> faceimgdic = new Dictionary<string, Sprite>();

    /// <summary>
    /// 当前的pd组件
    /// </summary>
    private PlayableDirector director;
    private bool ifplay;


    void GetSentencesFromFile()
    {
        sentences.Clear();
        currentindex = 0;
        string[] lines = dialogs.text.Split('\n');
        foreach (string line in lines)
        {
            sentences.Add(line);
        }
    }

    void GetNpcinfo()
    {
        foreach (var info in characterinfos.npc)
        {
            faceimgdic.Add(info.name, info.faceimg);
        }
    }

    void SetDialogueInfo(DialoguePanel o)
    {
        //windows /r /n
        string content = sentences[currentindex].Trim().ToString();
        if (faceimgdic.ContainsKey(content))
        {
            if(faceimgdic[content]!=null)
            {
                o.GetComponent<Image>(PanelName.face).color = new Color(1, 1, 1, 1);
                o.GetComponent<Image>(PanelName.face).sprite = faceimgdic[content];
            }
            else
            {
                o.GetComponent<Image>(PanelName.face).color=new Color(1,1,1,0);
            }
            o.GetComponent<Text>(PanelName.name).text = content;
            currentindex++;
        }
        content = sentences[currentindex].Trim().ToString();
        o.GetComponent<Text>(PanelName.content).text = content;
        currentindex++;
        //Debug.Log(sentences.Count + " " + currentindex);
    }


    /// <summary>
    /// 该TimeLine Awake播放时、从头Play时
    /// </summary>
    /// <param name="playable"></param>
    public override void OnPlayableCreate(Playable playable)
    {
        UIManager.GetInstance().HidePanel(PanelName.dialoguepanel);
        director = playable.GetGraph().GetResolver() as PlayableDirector;
        if (faceimgdic.Count == 0)
        {
            characterinfos = Resources.Load("CharacterInfos") as CharacterInfos;
            GetNpcinfo();
        }
        GetSentencesFromFile();
    }

    /// <summary>
    /// 当时间轴在该代码片段时，每帧执行
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        Debug.Log("update");
        if (currentindex == sentences.Count)
        {
            UIManager.GetInstance().HidePanel(PanelName.dialoguepanel);
            currentindex = 0;
            TimelineManager.GetInstance().ResumeTimeLine();
            return;
        }
        if (Input.GetKeyDown(PanelName.continuekey) && currentindex < sentences.Count)
        {
            UIManager.GetInstance().ShowPanel<DialoguePanel>(PanelName.dialoguepanel, SetDialogueInfo);
        }
    }

    /// <summary>
    /// 当时间轴在该代码区域：Play、Resume时执行一次相当于onstateenter
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log("enter");
        UIManager.GetInstance().ShowPanel<DialoguePanel>(PanelName.dialoguepanel, SetDialogueInfo);
        TimelineManager.GetInstance().PauseTimeLine(director);
        ifplay = true;
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
        Debug.Log("pause");
        //当时间轴驶出该代码区域时执行
        if (ifplay)
        {
            UIManager.GetInstance().HidePanel(PanelName.dialoguepanel);
            ifplay = false;
        }
    }

    public override void OnPlayableDestroy(Playable playable)
    {
        UIManager.GetInstance().HidePanel(PanelName.dialoguepanel);
        currentindex = 0;
    }

}

