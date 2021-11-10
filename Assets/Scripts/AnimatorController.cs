using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���ﶯ��״̬����һЩ��������
/// </summary>
public class AnimParms
{
    /// <summary>
    /// �Ƿ��ƶ��Ĳ���
    /// </summary>
    public static string IsWalking = "IsWalking";

    /// <summary>
    /// �ſ��ش�����
    /// </summary>
    public static string triggerdoor = "triggerdoor";

    /// <summary>
    /// ����״̬
    /// </summary>
    public static string IsCouching = "IsCouching";

    /// <summary>
    /// ʰȡ����¶���
    /// </summary>
    public static string Pickup = "Armed";

    /// <summary>
    /// ��Ծ
    /// </summary>
    public static string Jump = "Jump";
    public static string Land = "Land";
    public static string Roll = "roll";


    public static string horizontal = "horizontal";
    public static string vertical = "vertical";

    public static string aiming = "Armed";
    public static string fire = "Shoot";

    public static string gethurt = "GetHurt";
    public static string resume = "Resume";
}




public class AnimatorController : MonoBehaviour
{
    protected Animator anim;

    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null) Debug.LogError("animcontroller not found!");
    }
}
