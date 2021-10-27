using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperaManager : MonoBehaviour
{
    private static OperaManager instance;
    public static OperaManager GetInstance() => instance;

    private Animator operaanim;
    private int currentname;

    private void Awake()
    {
        instance = this;
        operaanim = GetComponent<Animator>();
        if (operaanim == null) Debug.LogError("anim not set!");
        //DontDestroyOnLoad(this.gameObject);
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
