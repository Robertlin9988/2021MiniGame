using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savesettings
{
    public static string operastate = "operastate";
    public static string operastatename = "operastatename";
    public static string birthpoint = "birthpoint";
    public static string mirrorstate = "mirrorstate";
    public static string scenestate = "scenestate";
}



public class SaveOperaState : StateMachineBehaviour
{
    public enum saveway
    {
        saveonenter,saveonexit
    }

    public saveway way;
    public int playerbornplaceindex;


    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (way == saveway.saveonexit)
        {
            //��intʱɾname ��֮��nameɾint ����name������operamanager��
            PlayerPrefs.SetInt(savesettings.operastate, stateInfo.fullPathHash);
            if(PlayerPrefs.HasKey(savesettings.operastatename))
            {
                Debug.Log("delete name");
                PlayerPrefs.DeleteKey(savesettings.operastatename);
            }
            PlayerPrefs.SetInt(savesettings.birthpoint, playerbornplaceindex);
            PlayerPrefs.Save();
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(way==saveway.saveonenter)
        {
            PlayerPrefs.SetInt(savesettings.operastate, stateInfo.fullPathHash);
            if (PlayerPrefs.HasKey(savesettings.operastatename))
            {
                Debug.Log("delete name");
                PlayerPrefs.DeleteKey(savesettings.operastatename);
            }
            PlayerPrefs.SetInt(savesettings.birthpoint, playerbornplaceindex);
            PlayerPrefs.Save();
        }
    }
}
