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
