using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] //so it's show in Unity as a list
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0.0001f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    public bool ignoreListenerPause; //does not off when game pause

    public AudioMixerGroup audioMixer;

    [HideInInspector] //hide for Inspector in Unity 
    public AudioSource source;
}
