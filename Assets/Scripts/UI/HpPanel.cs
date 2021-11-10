using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum hurtname
{
    playerhurt,
    enemyhurt
}

public class HpPanel : BasePanel
{
    private Slider hpslider;
    public hurtname target;
    public float fullhp=8;

    /// <summary>
    ///  ’µΩ…À∫¶
    /// </summary>
    /// <param name="value"></param>
    public void Gethurt(float value)
    {
        hpslider.value -= value/fullhp;
    }

    public override void OnPanelShow()
    {
        base.OnPanelShow();
        hpslider = this.GetComponent<Slider>(this.gameObject.name);
        hpslider.value = 1;
        EventCenter.GetInstance().AddEventListener<float>(target.ToString(), Gethurt);
    }



    protected override void Awake()
    {
        base.Awake();
    }


}
