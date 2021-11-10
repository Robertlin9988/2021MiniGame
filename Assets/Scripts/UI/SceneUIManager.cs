using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 需要初始化的一些Panel
/// </summary>
public enum Panel
{
    None,
    HpPanel,
    EnemyHpPanel,
    PausePanel,
    EndingPanel,
    StartMenuPanel
}


public class SceneUIManager : MonoBehaviour
{
    public Panel[] awakepanel;

    void ShowPausePanel()
    {
        Debug.Log("esc!");
        if (UIManager.GetInstance().GetPanel<PausePanel>(PanelName.pausepanel) == null)
        {
            UIManager.GetInstance().ShowPanel<PausePanel>(PanelName.pausepanel);
        }
        else
        {
            UIManager.GetInstance().HidePanel(PanelName.pausepanel);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        foreach (Panel panel in awakepanel)
        {
            switch(panel)
            {
                case Panel.HpPanel:
                    UIManager.GetInstance().ShowPanel<HpPanel>(Panel.HpPanel.ToString());
                    break;
                case Panel.EnemyHpPanel:
                    UIManager.GetInstance().ShowPanel<HpPanel>(Panel.EnemyHpPanel.ToString());
                    break;
                case Panel.PausePanel:
                    //开启输入检测
                    InputMgr.GetInstance().EnableKeyDetect(true);
                    //暂停菜单
                    EventCenter.GetInstance().AddEventListener(EventName.escbuttonclicked, ShowPausePanel);
                    break;
                case Panel.EndingPanel:
                    UIManager.GetInstance().ShowPanel<EndingPanel>(Panel.EndingPanel.ToString());
                    break;
                case Panel.StartMenuPanel:
                    UIManager.GetInstance().ShowPanel<StartMenuPanel>(Panel.StartMenuPanel.ToString());
                    break;
            }
        }
    }
}
