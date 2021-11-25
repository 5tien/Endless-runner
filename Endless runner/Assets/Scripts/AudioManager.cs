﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    public AudioClip[] audioClips;

    public AudioMixer mixer;

    [Header("Audio Source's")]
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


    // ========== the SetVolumeLevel is not my own code. i took it from a youtube video https://www.youtube.com/watch?v=xNHSGMKtlv4&t=2s&ab_channel=JohnFrench  ======================
    /// <summary>
    /// changes the mastervolumemixers it value to the slider value
    /// </summary>
    /// <param name="sliderValue">the value of the volume slider</param>
    public void SetVolumeLevel(float sliderValue)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        Debug.Log("Hello there");
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
