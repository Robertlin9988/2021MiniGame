using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogChoice : StateMachineBehaviour
{
    [Header("问题")]
    public string question;

    [Header("选项")]
    public List<string> choices;

    List<GameObject> buttonadded=new List<GameObject>();

    Animator animcontroller;

    void onclickevent(string name)
    {
        animcontroller.SetTrigger(name);
    }


    void SetChoiceInfo(ChoicePanel o)
    {
        o.GetComponent<Text>(PanelName.question).text = question;
        Transform choicesgroup = o.GetComponent<GridLayoutGroup>(PanelName.choicesgroup).transform;
        GameObject choicebutton = Resources.Load(PanelName.choicebutton) as GameObject;
        for (int i=0;i<choices.Count;i++)
        {
            GameObject tmpbutton = Instantiate(choicebutton, choicesgroup);
            tmpbutton.name = PanelName.choicebutton + i.ToString();
            buttonadded.Add(tmpbutton);
            tmpbutton.GetComponentInChildren<Text>().text = choices[i];
            tmpbutton.GetComponent<Button>().onClick.AddListener(() =>
            {
                onclickevent(tmpbutton.name);//注意闭包值为最终值
            });
        }
    }


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(choices.Count==0)
        {
            Debug.LogError("Choices not set");
            return;
        }
        animcontroller = animator;
        UIManager.GetInstance().ShowPanel<ChoicePanel>(PanelName.choicepanel, SetChoiceInfo);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        UIManager.GetInstance().HidePanel(PanelName.choicepanel);
        foreach(GameObject tmp in buttonadded)
        {
            Destroy(tmp);
        }
        buttonadded.Clear();
    }
}
