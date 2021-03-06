﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer master = null;
    [SerializeField] AudioSource mainAudioSource = null;
    public float speedFadeTime;

    public AudioSource MainAudioSource { get => mainAudioSource; }

    public float Geral { get; set; }
    public float Effect { get; set; }
    public bool MuteValue { get; set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Ja existe um AudioManager");
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public void PlayMusic(AudioClip sound, bool fade = true)
    {
        if (fade)
        {
            StartCoroutine(Fade(sound));
        }
        else
        {
            MainAudioSource.clip = sound;
            MainAudioSource.Play();
        }
    }
    public void StopSound()
    {
        MainAudioSource.Stop();
    }
    public void FadeMusic()
    {
        StartCoroutine(Fade(null));
    } 
    IEnumerator Fade(AudioClip sound)
    {
        float fadeTime = 0;
        while (fadeTime < 1)
        {
            MainAudioSource.volume = Mathf.Lerp(1, 0, fadeTime);
            fadeTime += speedFadeTime;

            yield return null;
        }
        MainAudioSource.Stop();
        MainAudioSource.volume = 1;
        if(sound != null)
        {
            MainAudioSource.clip = sound;
            MainAudioSource.Play();
        }
    }

    //VOLUME CONTROL--------------------------------
    public void MusicVolume(float volume)
    {
        if (volume == -20)
        {
            volume = -80;
        }

        master.SetFloat("Music", volume);
    }

    public void DialogVolume(float volume)
    {
        if (volume == -20)
        {
            volume = -80;
        }

        master.SetFloat("Dialog", volume);
    }

    public void EffectsVolume(float volume)
    {
        if (volume == -20)
        {
            volume = -80;
        }

        master.SetFloat("Effects", volume);

        Effect = volume;
    }

    public void GeralVolume(float volume)
    {
        if (volume == -20)
        {
            volume = -80;
        }

        master.SetFloat("Geral", volume);

        Geral = volume;
    }

    public void Mute(bool mute)
    {
        if (mute) 
        {
            master.SetFloat("Mute", -80);
        }
        else
        {
            master.SetFloat("Mute", 0);
        }

        MuteValue = mute;
    }
}
