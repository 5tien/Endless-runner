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

    /// <summary>
    /// plays a sound ones
    /// </summary>
    /// <param name="number">the number of the audioclip in the audioclips array</param>
    public void PlaySoundEffect(int number)
    {
        soundEffectPlayer.PlayOneShot(audioClips[number]);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="number"></param>
    public void PlayBackGroundMusic(int number)
    {
        backgroundMusicPlayer.loop = true;
        backgroundMusicPlayer.clip = audioClips[number];
        backgroundMusicPlayer.Play();
    }
}
