using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



/// <summary>
/// ͳһ����BGM����Ч�Ĳ���
/// ����������ΨһAudiosource����
/// ��Ч����������Դ������
/// </summary>
public class AudioManager : BaseManagerMono<AudioManager>
{
    //Ψһ�ı����������
    private static AudioSource backgroundsource;
    //ȫ����Ч�������
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
    /// ���ű�������
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
    /// ��ͣ��������
    /// </summary>
    public void PauseBGM()
    {
        if (backgroundsource == null) return;
        backgroundsource.Pause();
    }

    /// <summary>
    /// ֹͣ��������
    /// </summary>
    public void StopBGM()
    {
        if (backgroundsource == null) return;
        backgroundsource.Stop();
    }

    /// <summary>
    /// ���ı�����������
    /// </summary>
    /// <param name="vol"></param>
    public void ChangeBGMVolume(float vol)
    {
        if (backgroundsource == null) return;
        backgroundsource.volume = vol;
    }

    /// <summary>
    /// �ڹ̶��㴦����һ����Ч���꼴ͣ
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
    /// �ƶ�������Ч�Ĳ���
    /// ������������
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
    /// ����ȫ��2d��Ч
    /// </summary>
    /// <param name="name"></param>
    /// <param name="isloop"></param>
    /// <param name="vol"></param>
    public void PlaySFX(string name, bool isloop = false,float vol=1)
    {
        if (globalsfxobj == null||!SFXs.ContainsKey(name)) return;
        //�����ǰ��Ч���ڲ�����ֱ���޸���ز���
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
    /// �ı���Ч������С
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
    /// ֹͣ��Ч
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
        ///�����õ�����������Ϊ�ᶯ̬ɾ��
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
