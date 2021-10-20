using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// 单句话剧情无需玩家输入
/// </summary>
public class DialogClip : PlayableAsset
{
    [Header("单句话剧情无需玩家输入")]
    public DialogBehavior template = new DialogBehavior();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        //需要传递实例参数才能在面板修改
        Playable playable = ScriptPlayable<DialogBehavior>.Create(graph, template);
        return playable;
    }
}
