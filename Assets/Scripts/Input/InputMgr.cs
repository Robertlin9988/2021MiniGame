using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : BaseManagerMono<InputMgr>
{

    private static InputMgr instance;
    private bool enableinputdetect=true;
    private Dictionary<string, KeyCode> eventkeys = new Dictionary<string, KeyCode>();

    private InputKeys keydic;

    void CheckKeydown(KeyCode key,string eventname)
    {
        if (Input.GetKeyDown(key))
        {
            EventCenter.GetInstance().EventTrigger(eventname);
        }
    }

    void CheckKey(KeyCode key, string eventname)
    {
        if (Input.GetKey(key))
        {
            EventCenter.GetInstance().EventTrigger(eventname);
        }
    }

    public void EnableKeyDetect(bool state)
    {
        enableinputdetect = state;
    }

    public KeyCode GetKey(string keyname)
    {
        return eventkeys[keyname];
    }

    // Start is called before the first frame update
    void Start()
    {
        keydic = Resources.Load(InputKeys.fileName) as InputKeys;
        if (keydic == null) Debug.LogError("Key profile object not set!");
    }

    // Update is called once per frame
    void Update()
    {
        //如未开启按键检测则返回
        if (!enableinputdetect) return;

        foreach (KeyEvents keyevent in keydic.keyevents)
        {
            CheckKeydown(keyevent.key, keyevent.eventname.ToString()+"按下");
            CheckKey(keyevent.key, keyevent.eventname.ToString() + "按着");
        }
    }
}
