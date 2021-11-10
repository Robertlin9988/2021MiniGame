using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum Slidername
{
    BulletBlue,
    BulletGreen,
    BulletRed
}

[Serializable]
public struct SliderPair
{
    public Slidername name;
    public bulletcolor color;
}



public class BulletPanel : BasePanel
{
    public SliderPair[] sliders;
    public float fullnum = 8;
    private Slider currentslider;
    private Dictionary<string, string> sliderdic = new Dictionary<string, string>();

    void ChangeSliderValue(float value)
    {
        currentslider.value = value/fullnum;
    }

    void Charge(bullettype type,float num)
    {
        if(sliderdic.ContainsKey(type.color.ToString()))
        {
            currentslider.gameObject.SetActive(false);
            currentslider = this.GetComponent<Slider>(sliderdic[type.color.ToString()]);
            Debug.Log(currentslider);
            currentslider.value = num/fullnum;
            currentslider.gameObject.SetActive(true);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        foreach (SliderPair pair in sliders)
        {
            sliderdic.Add(pair.color.ToString(), pair.name.ToString());
            this.GetComponent<Slider>(pair.name.ToString()).gameObject.SetActive(false);
        }
        currentslider = GetComponent<Slider>(sliders[0].name.ToString());
        EventCenter.GetInstance().AddEventListener<float>(EventName.successshoot, ChangeSliderValue);
        EventCenter.GetInstance().AddEventListener<bullettype, float>(EventName.pickupbullets, Charge);
    }
}
