using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] // To make the class appear in the inspector
public class Sound {
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(.1f, 3f)]
    public float pitch = 1;

    public bool loop;
    public bool soundTrack;

    [HideInInspector]
    public AudioSource source;
}
