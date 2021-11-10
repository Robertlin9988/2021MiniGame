using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animevents : MonoBehaviour
{
    public GameObject bow;
    public GameObject righthand;

    private bool triggerstart;
    private Vector3 oripos;
    private Quaternion orirotation;

    private static Animevents instance;
    public static Animevents GetInstance() => instance;


    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        oripos = bow.transform.localPosition;
        orirotation = bow.transform.localRotation;
    }


    private void Update()
    {
        if(triggerstart)
        {
            bow.transform.position = righthand.transform.position;
        }
    }

    public void bowtriggerstart()
    {
        triggerstart = true;
    }

    public void bowtriggerexit()
    {
        triggerstart = false;
        bow.transform.localPosition = oripos;
        bow.transform.localRotation = orirotation;
    }
}
