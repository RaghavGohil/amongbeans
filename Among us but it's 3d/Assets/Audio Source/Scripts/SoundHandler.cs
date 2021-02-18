using UnityEngine;
using System;
using UnityEngine.Audio;

[System.Serializable]

public class Sound
{
    public string name;
    public AudioClip clip;
    public float volume;                                  //SoundManagerScript[Responsible for sounds in the game.]
    public float pitch;
    public AudioSource src;
    public bool loopable;
}

public class SoundHandler : MonoBehaviour
{   

    public Sound[] SoundStorage;

    private void Start()
    {

        foreach (Sound S in SoundStorage)
        {
            S.src = gameObject.AddComponent<AudioSource>();
            S.src.clip = S.clip;
            S.src.name = S.name;
            S.src.pitch = S.pitch;       //Add main infos to new component.
            S.src.loop = S.loopable;
        }

    }
    
    public void PlaySFX(string name)
    {
        if (name != null)
        {
            Sound s = Array.Find(SoundStorage, Sound => Sound.name == name);
            s.src.Play();
        }
        else
        {
            Debug.LogError("[:Sys:]"+" Error "+name+" not initialized.");
        }
    }

    public void StopSFX(string name)
    {

        if (name != null)
        {
            Sound s = Array.Find(SoundStorage, Sound => Sound.name == name);
            s.src.Stop();
        }
        else
        {
            Debug.LogError("[:Sys:]"+" Error "+name+" not initialized.");
        }

    }

    //Sound Functions

    public void OnClickPlayButtonSound()
    {

        PlaySFX("ButtonSound");

    }

}
