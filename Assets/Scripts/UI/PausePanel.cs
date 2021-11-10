using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnPanelShow()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    public override void OnPanelHide()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    protected override void OnClick(string name)
    {
        base.OnClick(name);
        switch(name)
        {
            case "Continue":
                UIManager.GetInstance().HidePanel(PanelName.pausepanel);
                break;
            case "Backtomainmenu":
                Time.timeScale = 1;
                ScenneManagement.GetInstance().LoadSceneSingle(0);
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
