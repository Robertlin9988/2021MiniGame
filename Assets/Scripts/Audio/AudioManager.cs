using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



/// <summary>
/// 统一管理BGM和音效的播放
/// 背景音乐由唯一Audiosource播放
/// 音效依附到播放源对象上
/// </summary>
public class AudioManager : BaseManagerMono<AudioManager>
{
    //唯一的背景音乐组件
    private static AudioSource backgroundsource;
    //全局音效播放组件
    private GameObject globalsfxobj;
    private Dictionary<string, AudioSource> sfxsources = new Dictionary<string, AudioSource>();

    private Dictionary<string, AudioClip> BGMs = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> SFXs = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        AudioResources audioobject = Resources.Load(AudiosName.objname) as AudioResources;
        if (audioobject == null) Debug.LogError("audioobject not found in resources!");
        foreach (AudioInfo bgm in audioobject.BGM)
        {
            BGMs.Add(bgm.name, bgm.clip);
        }
        foreach (AudioInfo sfx in audioobject.SFX)
        {
            SFXs.Add(sfx.name, sfx.clip);
        }
        if (backgroundsource == null)
        {
            GameObject bgmsource = new GameObject();
            bgmsource.name = "bgmsource";
            backgroundsource = bgmsource.AddComponent<AudioSource>();
            DontDestroyOnLoad(bgmsource);
        }
        if (globalsfxobj == null)
        {
            globalsfxobj = new GameObject();
            globalsfxobj.name = "globalsfxobj";
            DontDestroyOnLoad(globalsfxobj);
        }
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isloop"></param>
    /// <param name="volume"></param>
    public void PlayBGM(string name,bool isloop=true,float volume=1)
    {
        if(BGMs.ContainsKey(name))
        {
            backgroundsource.clip = BGMs[name];
            backgroundsource.loop = isloop;
            backgroundsource.volume = volume;
            if(!backgroundsource.isPlaying) backgroundsource.Play();
        }
    }

    /// <summary>
    /// 暂停背景音乐
    /// </summary>
    public void PauseBGM()
    {
        if (backgroundsource == null) return;
        backgroundsource.Pause();
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBGM()
    {
        if (backgroundsource == null) return;
        backgroundsource.Stop();
    }

    /// <summary>
    /// 更改背景音乐音量
    /// </summary>
    /// <param name="vol"></param>
    public void ChangeBGMVolume(float vol)
    {
        if (backgroundsource == null) return;
        backgroundsource.volume = vol;
    }

    /// <summary>
    /// 在固定点处播放一个音效播完即停
    /// </summary>
    /// <param name="name"></param>
    /// <param name="position"></param>
    /// <param name="vol"></param>
    public void PlaySFXAtPoint(string name,Vector3 position, float vol = 1)
    {
        if(SFXs.ContainsKey(name))
        {
            AudioSource.PlayClipAtPoint(SFXs[name], position, vol);
        }
    }

    /// <summary>
    /// 移动对象音效的播放
    /// 依附到对象上
    /// </summary>
    /// <param name="name"></param>
    /// <param name="source"></param>
    /// <param name="vol"></param>
    /// <param name="isloop"></param>
    public void PlaySFXAttachToSource(string name,AudioSource source,float vol=1,bool isloop=false)
    {
        if(SFXs.ContainsKey(name))
        {
            source.clip = SFXs[name];
            source.loop = isloop;
            source.volume = vol;
            if (!source.isPlaying) source.Play();
        }
    }

    /// <summary>
    /// 播放全局2d音效
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isloop"></param>
    /// <param name="vol"></param>
    public void PlaySFX(string name, bool isloop = false,float vol=1)
    {
        if (globalsfxobj == null||!SFXs.ContainsKey(name)) return;
        //如果当前音效正在播放则直接修改相关参数
        if(!sfxsources.ContainsKey(name))
        {
            AudioSource source = globalsfxobj.AddComponent<AudioSource>();
            source.clip = SFXs[name];
            source.loop = isloop;
            source.volume = vol;
            source.Play();
            sfxsources.Add(name, source);
        }
        else
        {
            sfxsources[name].loop = isloop;
            sfxsources[name].volume = vol;
        }
    }

    /// <summary>
    /// 改变音效声音大小
    /// </summary>
    /// <param name="value"></param>
    public void ChangeSoundValue(string name,float value)
    {
        if(sfxsources.ContainsKey(name))
        {
            sfxsources[name].volume = value;
        }
    }

    /// <summary>
    /// 停止音效
    /// </summary>
    public void StopSound(string name)
    {
        if (sfxsources.ContainsKey(name))
        {
            AudioSource source = sfxsources[name];
            source.Stop();
            GameObject.Destroy(source);
            sfxsources.Remove(name);
        }
    }

    public void StopAll()
    {
        if(backgroundsource!=null) backgroundsource.Stop();
        for (int i = 0; i < sfxsources.Count; i++)
        {
            var item = sfxsources.ElementAt(i);
            GameObject.Destroy(item.Value);
            sfxsources.Remove(item.Key);
        }
    }


    private void Update()
    {
        ///不能用迭代器迭代因为会动态删除
        for(int i=0;i<sfxsources.Count;i++)
        {
            var item=sfxsources.ElementAt(i);
            if(!item.Value.isPlaying)
            {
                GameObject.Destroy(item.Value);
                sfxsources.Remove(item.Key);
            }
        }
    }

}
