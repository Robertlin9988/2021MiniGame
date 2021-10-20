using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 从Txt读取剧情 需要玩家键入操作
/// </summary>
public class DialogPassages : PlayableAsset
{
    [Header("对话内容")]
    public DialogBehaviorTxt template = new DialogBehaviorTxt();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        //需要传递实例参数才能在面板修改
        Playable playable = ScriptPlayable<DialogBehaviorTxt>.Create(graph, template);
        return playable;
    }
}
