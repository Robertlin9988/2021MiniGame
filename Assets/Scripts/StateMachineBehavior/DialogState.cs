using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public struct NPCInfo
{
    public string name;
    public Sprite faceimg;
}


public class DialogState : StateMachineBehaviour
{
    [Header("对话内容txt文本")]
    public TextAsset dialogs;

    [Header("人物信息")]
    public NPCInfo[] npc;

    //当前段落行数
    int currentindex;
    //存储段落的一行行文本
    List<string> sentences = new List<string>();
    //人物名字对应的头像
    Dictionary<string, Sprite> faceimgdic = new Dictionary<string, Sprite>();

    void GetSentencesFromFile()
    {
        sentences.Clear();
        currentindex = 0;
        string[] lines=dialogs.text.Split('\n');
        foreach(string line in lines)
        {
            sentences.Add(line);
        }
    }

    void GetNpcinfo()
    {
        faceimgdic.Clear();
        foreach (var info in npc)
        {
            faceimgdic.Add(info.name, info.faceimg);
        }
    }

    void SetDialogInfo(DialoguePanel o)
    {
        //windows /r /n
        string content= sentences[currentindex].Trim().ToString();
        if(faceimgdic.ContainsKey(content))
        {
            o.GetComponent<Image>(PanelName.face).sprite = faceimgdic[content];
            o.GetComponent<Text>(PanelName.name).text = content;
            currentindex++;
        }
        content = sentences[currentindex].Trim().ToString();
        o.GetComponent<Text>(PanelName.content).text = content;
        currentindex++;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetSentencesFromFile();
        GetNpcinfo();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (currentindex == sentences.Count)
        {
            UIManager.GetInstance().HidePanel(PanelName.dialoguepanel);
            currentindex = 0;
            animator.SetTrigger(AnimParam.statetrans);
            return;
        }
        if (Input.GetKeyDown(PanelName.continuekey)&&currentindex<sentences.Count)
        {
            UIManager.GetInstance().ShowPanel<DialoguePanel>(PanelName.dialoguepanel, SetDialogInfo);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UIManager.GetInstance().HidePanel(PanelName.dialoguepanel);
        currentindex = 0;
    }
}
