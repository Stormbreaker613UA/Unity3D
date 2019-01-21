using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Audio
{
    #region Private_Variables
       
    private AudioSource sourceSFX; //Ссылка на источник звука для воспроизведения звуков
    
    private AudioSource sourceMusic; //Ссылка на источник звука для воспроизведения музыки
    
    private AudioSource sourceRandomPitchSFX; //Ссылка на источник звука для воспроизведения звуков со случайной частотой
    
    private float musicVolume = 1f; //громкость музыки
    
    private float sfxVolume = 1f; //громкость звуков
    
    [SerializeField]
    private AudioClip[] sounds; //Массив звуков

    [SerializeField]
    private AudioClip[] JumpSounds; //Массив звуков прыжков

    [SerializeField]
    private AudioClip[] FootstepSounds; //Массив звуков шагов

    [SerializeField]
    private AudioClip landSound; //Звук приземления 

    [SerializeField]
    private AudioClip defaultClip; //Звук по умолчанию, на случай, если в массиве отсутствует требуемый
    
    [SerializeField]
    private AudioClip menuMusic; //Музыка для главного меню
    
    [SerializeField]
    private AudioClip gameMusic; //Музыка для игры на уровнях

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = value;
            SourceMusic.volume = musicVolume;
        }
    }
    public float SfxVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = value;
            SourceSFX.volume = sfxVolume;
            SourceRandomPitchSFX.volume = sfxVolume;
        }
    }

    public AudioSource SourceSFX
    {
        get { return sourceSFX; }
        set
        {
            sourceSFX = value;
        }
    }
    public AudioSource SourceMusic
    {
        get { return sourceMusic; }
        set
        {
            sourceMusic = value;
        }
    }
    public AudioSource SourceRandomPitchSFX
    {
        get { return sourceRandomPitchSFX; }
        set
        {
            sourceRandomPitchSFX = value;
        }
    }
    #endregion

    private AudioClip GetSound(string clipName)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == clipName)
            {
                return sounds[i];
            }
        }
        Debug.LogError("Can not find clip " + clipName);
        return defaultClip;
    }

    public void PlaySoundFX(string clipName)
    {
        SourceSFX.PlayOneShot(GetSound(clipName), SfxVolume);
    }

    public void PlayJumpSound()
    {   
        SourceSFX.PlayOneShot(JumpSounds[Random.Range(0, JumpSounds.Length)], SfxVolume);
    }

    public void PlayFootstepSounds()
    {
        SourceSFX.PlayOneShot(FootstepSounds[Random.Range(0, JumpSounds.Length)], SfxVolume);
    }

    public void PlayLandSound()
    {
        SourceSFX.PlayOneShot(landSound, SfxVolume);
    }

    public void PlaySoundRandomPitch(string clipName)
    {
        SourceRandomPitchSFX.pitch = Random.Range(0.7f, 1.3f);
        SourceRandomPitchSFX.PlayOneShot(GetSound(clipName), SfxVolume);
    }

    public void PlayMusic(bool menu)
    {
        if (menu)
        {
            SourceMusic.clip = menuMusic;
        }
        else
        {
            SourceMusic.clip = gameMusic;
        }
        SourceMusic.volume = MusicVolume;
        SourceMusic.loop = true;
        SourceMusic.Play();
    }


}
