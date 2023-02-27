using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    List<int> soundTrackIndexes = new List<int>();
    int currentSoundTrack;
    Sound currentTrack;

    public static AudioManager instance;

    void Awake() {

        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < sounds.Length; i++) {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;

            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
            sounds[i].source.loop = sounds[i].loop;

			if (sounds[i].soundTrack) {
                soundTrackIndexes.Add(i);
			}
        }
    }

    private void Start() {
		currentSoundTrack = 0;
		currentTrack = sounds[soundTrackIndexes[currentSoundTrack]];
		Play(currentTrack.name);
    }

    void Update() {
        if (soundTrackIndexes.Count == 0)
            return;

		if (!currentTrack.source.isPlaying) {
			currentSoundTrack++;

			if (currentSoundTrack >= soundTrackIndexes.Count) {
				currentSoundTrack = 0;

			}

			currentTrack = sounds[soundTrackIndexes[currentSoundTrack]];
			Play(currentTrack.name);
		}
	}

    public void Play (string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);//Error
        if (s == null)
        {
            print("Audio \"" + name + "\" not found");
            return;
        }
        
        s.source.Play();

        //Debug.Log("PLAYING");
    }
}
