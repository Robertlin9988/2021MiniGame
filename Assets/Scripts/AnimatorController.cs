using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ���ﶯ��״̬����һЩ��������
/// </summary>
public class AnimParms
{
    //�Ƿ��ƶ��Ĳ���
    public static string IsWalking = "IsWalking";

    //�ſ��ش�����
    public static string triggerdoor = "triggerdoor";
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
