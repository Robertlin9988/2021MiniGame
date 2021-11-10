using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEventTrigger : MonoBehaviour
{
    private void Start()
    {
        Button btn = this.GetComponent<Button>();
        UIEventListener btnListener = btn.gameObject.AddComponent<UIEventListener>();

        btnListener.OnClick += delegate (GameObject gb) {
            Debug.Log(gb.name + " OnClick");
            AudioManager.GetInstance().PlaySFX(AudiosName.buttonclicked);
            switch(gb.name)
            {
                case "Start":
                    ScenneManagement.GetInstance().LoadSceneSingle(0);
                    break;
                case "Quite":
                    Application.Quit();
                    Debug.Log("quit");
                    break;
            }
        };

        btnListener.OnMouseEnter += delegate (GameObject gb) {
            AudioManager.GetInstance().PlaySFX(AudiosName.continuebutton);
        };

        btnListener.OnMouseExit += delegate (GameObject gb) {
            Debug.Log(gb.name + " OnMOuseExit");
        };
    }
}
