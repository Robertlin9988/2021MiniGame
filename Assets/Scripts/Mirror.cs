using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private bool[] state= { true,true,true};
    public int mymirrorindex;
    public GameObject fallingitems;


    public void Savemirorstate(int index)
    {
        PlayerPrefs.SetInt(savesettings.mirrorstate,index);
    }


    private void Awake()
    {
        if(PlayerPrefs.HasKey(savesettings.mirrorstate))
        {
            for(int i=0;i<=PlayerPrefs.GetInt(savesettings.mirrorstate);i++)
            {
                state[i] = false;
            }
        }

        if(!state[mymirrorindex])
        {
            gameObject.SetActive(false);
            fallingitems.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
