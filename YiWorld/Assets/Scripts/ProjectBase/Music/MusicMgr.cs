/****************************************************
    文件：MusicMgr.cs
	作者：UnitySir
    邮箱: zoujack@foxmail.com
    日期：2020/4/18 21:14:49
	功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

public class MusicMgr : BaseManager<MusicMgr>
{
    private AudioSource bgMusic;
    private float bgValue = 1f;

    private GameObject soundObj = null;
    private List<AudioSource> soundList = new List<AudioSource>();
    private float soundValue = 1f;


    public MusicMgr()
    {
        MonoMgr.GetInstance().AddUpdateListener(()=>
        {
            for (int i = soundList.Count - 1; i >= 0; --i)
            {
                if (!soundList[i].isPlaying)
                {
                    GameObject.Destroy(soundList[i]);
                    soundList.RemoveAt(i);
                    
                }
            }
        });
    }


    public void PlayBGMusic(string name)
    {
        if(bgMusic == null)
        {
            GameObject obj = new GameObject();
            obj.name = "BGMusic";
            bgMusic = obj.AddComponent<AudioSource>();
        }
        ResMgr.GetInstance().LoadAsync<AudioClip>("ResAudio/"+name, (ac)=>
        {
            bgMusic.clip = ac;
            bgMusic.volume = bgValue;
            bgMusic.loop = true;
            bgMusic.Play();
        });
    }
    public void PauseMusic()
    {
        if (bgMusic == null) return;
        bgMusic.Pause();
    }
    public void StopBGMusic()
    {
        if (bgMusic == null) return;
        bgMusic.Stop();
    }
    public void ChangeBGValue(float val)
    {
        bgValue = val;
        if (bgValue == null) return;
        bgMusic.volume = bgValue;
    }



    public void PlayerSound(string name,bool isLoop,Action<AudioSource> callBack=null)
    {
        if(soundObj == null)
        {
            soundObj = new GameObject();
            soundObj.name = "Sound";
        }
        ResMgr.GetInstance().LoadAsync<AudioClip>("ResAudio/" + name, (ac) =>
        {
            AudioSource source = soundObj.AddComponent<AudioSource>();
            source.clip = ac;
            source.loop = isLoop;
            source.volume = soundValue;
            source.Play();
            soundList.Add(source);
            if (callBack != null)
                callBack(source);
        });
    }
    public void ChangeSoundValue(float val)
    {
        soundValue = val;
        foreach (var sound in soundList)
        {
            sound.volume = val;
        }
    }

    public void StopSound(AudioSource source)
    {
        if (soundList.Contains(source))
        {
            soundList.Remove(source);
            source.Stop();
            GameObject.Destroy(source);
        }
    }
}