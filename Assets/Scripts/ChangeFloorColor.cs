using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorColor : MonoBehaviour
{
    private static ChangeFloorColor instance;
    public static ChangeFloorColor GetInstance() => instance;

    public Color[] colors;
    public Color[] emissioncolors;
    public Material mat;

    public GameObject[] shield;

    public int colorindex = 0;
    public int lastindex = 2;

    private GameObject aliveshield;

    private void Awake()
    {
        instance = this;
        mat.SetColor("_EmissionColor2", emissioncolors[colorindex]);
        mat.SetColor("_Color2", colors[colorindex]);
        mat.SetColor("_EmissionColor", emissioncolors[lastindex]);
        mat.SetColor("_Color", colors[lastindex]);
        mat.SetFloat("_BurnRadius", 0);
        shield[lastindex].SetActive(true);
        aliveshield = shield[lastindex];
    }

    public void SetColor()
    {
        int index = Random.Range(0, 2);
        colorindex = (index == lastindex) ? ((index + 1) % colors.Length) : index;
        //Debug.Log("curindex:" + colorindex + "  lastindex:" + lastindex);
        mat.SetColor("_EmissionColor2", emissioncolors[colorindex]);
        mat.SetColor("_Color2", colors[colorindex]);
        mat.SetColor("_EmissionColor", emissioncolors[lastindex]);
        mat.SetColor("_Color", colors[lastindex]);
        mat.SetFloat("_BurnRadius", 0);
        lastindex = colorindex;
    }

    public void ChangeShield()
    {
        aliveshield.SetActive(false);
        shield[colorindex].SetActive(true);
        aliveshield = shield[colorindex];
    }
}
