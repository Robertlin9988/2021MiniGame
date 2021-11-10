using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperaManager : MonoBehaviour
{
    private static OperaManager instance;
    private Animator operaanim;
    private int currentname;
    private static GameObject operamanager;
    private static string objname = "OperaState";

    public bool clearsaving = false;

    public static OperaManager GetInstance()
    {
        if (operamanager == null)
        {
            operamanager = GameObject.Find(objname);
            if (operamanager == null)
            {
                operamanager = new GameObject();
                operamanager.name = objname;
            }
        }
        if (instance == null)
        {
            instance = operamanager.GetComponent<OperaManager>();
            if (instance == null)
            {
                instance = operamanager.AddComponent<OperaManager>();
            }
        }
        return instance;
    } 

    private void Awake()
    {
        //debug only
        if(clearsaving)
        {
            PlayerPrefs.DeleteKey(savesettings.scenestate);
            PlayerPrefs.DeleteKey(savesettings.operastatename);
            PlayerPrefs.DeleteKey(savesettings.mirrorstate);
            PlayerPrefs.DeleteKey(savesettings.operastate);
            PlayerPrefs.DeleteKey(savesettings.birthpoint);
        }


        operaanim = GetComponent<Animator>();
        if (operaanim == null) Debug.LogError("anim not set!");
        if(PlayerPrefs.HasKey(savesettings.operastatename))
        {
            Debug.Log("play name");
            operaanim.Play(PlayerPrefs.GetString(savesettings.operastatename));
        }
        else if(PlayerPrefs.HasKey(savesettings.operastate))
        {
            operaanim.Play(PlayerPrefs.GetInt(savesettings.operastate));
        }
    }

    public void SetStateTrans()
    {
        operaanim.SetTrigger("statetrans");
    }


    public void PlayDialog(string name)
    {
        currentname = operaanim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        operaanim.Play(name);
    }

    public void ResumeMainDialog()
    {
        operaanim.Play(currentname);
    }


}
