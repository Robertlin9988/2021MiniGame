using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;


[Serializable]
public class DialogBehaviorTxt : PlayableBehaviour
{
    [Header("�Ի�����txt�ı�")]
    public TextAsset dialogs;

    //��ǰ��������
    int currentindex;
    //�洢�����һ�����ı�
    List<string> sentences = new List<string>();
    //�������ֶ�Ӧ��ͷ��
    static CharacterInfos characterinfos;
    static Dictionary<string, Sprite> faceimgdic = new Dictionary<string, Sprite>();

    /// <summary>
    /// ��ǰ��pd���
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
    /// ��TimeLine Awake����ʱ����ͷPlayʱ
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
    /// ��ʱ�����ڸô���Ƭ��ʱ��ÿִ֡��
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
    /// ��ʱ�����ڸô�������Play��Resumeʱִ��һ���൱��onstateenter
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
    /// ��ʱ�����ڸô�������Pause��Stopʱִ��һ��
    ///����ͷ���Ÿ�TimeLineʱִ��һ��
    ///��ʱ����ʻ���ô�������ʱִ��һ��
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Debug.Log("pause");
        //��ʱ����ʻ���ô�������ʱִ��
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

