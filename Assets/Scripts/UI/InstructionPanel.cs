using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionPanel : BasePanel
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnClick(string name)
    {
        base.OnClick(name);
        UIManager.GetInstance().HidePanel(PanelName.instructionpanel);
    }

    protected override void OnClick(GameObject obj)
    {
        base.OnClick(obj);
    }


    protected override void OnMouseEnter(GameObject obj)
    {
        base.OnMouseEnter(obj);
    }
}
