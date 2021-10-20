using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// ���仰���������������
/// </summary>
public class DialogClip : PlayableAsset
{
    [Header("���仰���������������")]
    public DialogBehavior template = new DialogBehavior();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        //��Ҫ����ʵ����������������޸�
        Playable playable = ScriptPlayable<DialogBehavior>.Create(graph, template);
        return playable;
    }
}
