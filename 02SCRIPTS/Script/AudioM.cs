using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public Sound[] deathSounds; // 죽음 사운드 배열 추가

    public Sound[] PanelOpen;

    private void Awake()
    {
        PlayMusic("Theme");

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, x => x.name == name);

        if ( sound == null)
        {
            Debug.Log("Sound Mot Found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Mot Found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

     public void PlayRandomDeathSFX()
    {
        if (deathSounds.Length == 0) return; // 사운드가 없으면 아무것도 하지 않음

        int randomIndex = UnityEngine.Random.Range(0, deathSounds.Length); // 랜덤 인덱스 선택
        Sound randomSound = deathSounds[randomIndex]; // 랜덤 사운드 선택

        if (randomSound != null)
        {
            sfxSource.PlayOneShot(randomSound.clip); // 랜덤 사운드 재생
        }
        else
        {
            Debug.Log("Random death sound not found");
        }
    }
}