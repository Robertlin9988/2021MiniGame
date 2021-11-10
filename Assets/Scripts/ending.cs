using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ending : MonoBehaviour
{
    public int sceneindex;
    public int birthindex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == PlayerController.mtag)
        {
            PlayerPrefs.SetString(savesettings.operastatename, "SecondSceneFinish");
            PlayerPrefs.SetInt(savesettings.birthpoint, 3);
            PlayerPrefs.SetInt(savesettings.mirrorstate, 0);
            ScenneManagement.GetInstance().LoadSceneSingle(sceneindex);
        }
    }
}
