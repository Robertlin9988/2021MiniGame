using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum statename
{
    FirstSceneFinish,
    SecondSceneFinish,
    ThirdSceneFinish
}

[Serializable]
public struct savestate
{
    public statename targetstate;
    public int birthindex;
    public int mirrorstate;
}


public class DebugSaveState : MonoBehaviour
{
    public bool clearsaving;
    public int targetscene;
    public savestate targetpoint;

    // Start is called before the first frame update
    private void Awake()
    {
        //debug only
        if (clearsaving)
        {
            PlayerPrefs.DeleteKey(savesettings.scenestate);
            PlayerPrefs.DeleteKey(savesettings.operastatename);
            PlayerPrefs.DeleteKey(savesettings.mirrorstate);
            PlayerPrefs.DeleteKey(savesettings.operastate);
            PlayerPrefs.DeleteKey(savesettings.birthpoint);
        }
        PlayerPrefs.SetInt(savesettings.scenestate, targetscene);
        PlayerPrefs.SetString(savesettings.operastatename, targetpoint.targetstate.ToString());
        PlayerPrefs.SetInt(savesettings.birthpoint, targetpoint.birthindex);
        PlayerPrefs.SetInt(savesettings.mirrorstate, targetpoint.mirrorstate);
    }
}
