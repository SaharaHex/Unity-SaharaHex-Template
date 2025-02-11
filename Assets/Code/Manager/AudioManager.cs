using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance; //to make one copy only

    //run before Start
    void Awake()
    {
        if (instance == null)
            instance = this;
        else //we have to so delete one
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); //to make sound continue to next scene 

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.ignoreListenerPause = s.ignoreListenerPause;
            s.source.outputAudioMixerGroup = s.audioMixer;
        }
    }

    private void Start()
    {
        PlaySound("ThemeSound");
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }
}
