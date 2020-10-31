using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class AudioSFXController : Singleton<AudioSFXController>
{
    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        m_audioSource.volume = PlayerDataManager.PlayerAccountData.m_sfxVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        m_audioSource.PlayOneShot(clip);
    }
}
