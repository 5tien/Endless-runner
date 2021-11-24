using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;
    public AudioClip[] audioClips;
    public AudioSource soundEffectPlayer;
    public AudioSource backgroundMusicPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        soundEffectPlayer.PlayOneShot(audioClip);
    }

    public void PlayBackGroundMusic(int number)
    {
        backgroundMusicPlayer.Play();
    }
}
