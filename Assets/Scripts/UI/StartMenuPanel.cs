using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnPanelShow()
    {
        base.OnPanelShow();
        Cursor.visible = true;
        if (PlayerPrefs.HasKey(savesettings.scenestate))
        {
            GetComponent<Text>("StartText").text = "重新开始";
            GetComponent<Button>("Continue").gameObject.SetActive(true);
        }
        else
        {
            GetComponent<Text>("StartText").text = "开始游戏";
            GetComponent<Button>("Continue").gameObject.SetActive(false);
        }
    }

    protected override void OnClick(string name)
    {
        base.OnClick(name);
        switch (name)
        {
            case "Start":
                ScenneManagement.GetInstance().LoadSceneSingle(1);
                break;
            case "Quite":
                Application.Quit();
                Debug.Log("quit");
                break;
            case "Continue":
                ScenneManagement.GetInstance().LoadSceneSingle(PlayerPrefs.GetInt(savesettings.scenestate));
                break;
            case "Instruction":
                UIManager.GetInstance().ShowPanel<InstructionPanel>(PanelName.instructionpanel);
                break;
        }
    }

    protected override void OnMouseEnter(GameObject obj)
    {
        base.OnMouseEnter(obj);
    }
}
