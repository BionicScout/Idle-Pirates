using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource musicSource;
    public AudioSource soundSource;

    public void Pause()
    {
        musicSource.Pause();
        soundSource.Pause();
    }




    public void UnPause()
    {

        musicSource.UnPause();
        soundSource.UnPause();

    }
}
