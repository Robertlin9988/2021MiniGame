using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


[Serializable]
public class DialogBehavior : PlayableBehaviour
{
    [Header("��������")]
    public string charactername;
    [Header("�Ի�����")]
    [TextArea(8, 1)] public string dialoguecontent;
    [Header("���ִ�С")]
    public int textsize;
    [Header("�Ƿ���Ҫ���ȷ��")]
    public bool pauseafterdialog;

    /// <summary>
    /// ��ǰƬ���Ƿ����ڲ���
    /// </summary>
    private bool isclipplay;
    /// <summary>
    /// �Ƿ���ͣ
    /// </summary>
    private bool ispauseschedule;
    /// <summary>
    /// ��ǰ��pd���
    /// </summary>
    private PlayableDirector director;


    void SetDialogueInfo(SentencePanel o)
    {
        o.GetComponent<Text>(PanelName.content).text = dialoguecontent;
        o.GetComponent<Text>(PanelName.content).fontSize = textsize;
    }


    /// <summary>
    /// ��TimeLine Awake����ʱ����ͷPlayʱ
    /// </summary>
    /// <param name="playable"></param>
    public override void OnPlayableCreate(Playable playable)
    {
        director = playable.GetGraph().GetResolver() as PlayableDirector;
    }    

    /// <summary>
    /// ��ʱ�����ڸô���Ƭ��ʱ��ÿִ֡��
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    /// <param name="playerData"></param>
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        //����δ���Ÿ�clip����ǰclip��Ȩ��>0
        //Ч����ʱ����ս����clipʱִ��һ��
        if (isclipplay==false && info.weight>0)
        {
            if (pauseafterdialog)
                ispauseschedule = true;
            isclipplay = true;
            UIManager.GetInstance().ShowPanel<SentencePanel>(PanelName.sentencepanel, SetDialogueInfo);
        }
    }


    /// <summary>
    /// ��ʱ�����ڸô�������Pause��Stopʱִ��һ��
    ///����ͷ���Ÿ�TimeLineʱִ��һ��
    ///��ʱ����ʻ���ô�������ʱִ��һ��
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
