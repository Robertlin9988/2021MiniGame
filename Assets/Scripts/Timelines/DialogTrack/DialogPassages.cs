using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// ��Txt��ȡ���� ��Ҫ��Ҽ������
/// </summary>
public class DialogPassages : PlayableAsset
{
    [Header("�Ի�����")]
    public DialogBehaviorTxt template = new DialogBehaviorTxt();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        //��Ҫ����ʵ����������������޸�
        Playable playable = ScriptPlayable<DialogBehaviorTxt>.Create(graph, template);
        return playable;
    }
}
