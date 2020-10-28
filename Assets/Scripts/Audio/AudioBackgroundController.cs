using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class AudioBackgroundController : Singleton<AudioBackgroundController>
{
    private AudioSource m_audioSource;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        m_audioSource.volume = GlobalSettings.m_musicVolume;
    }

    public void StartBackgroundMusic(GameMap toStart)
    {
        m_audioSource.Stop();

        m_audioSource.clip = toStart.m_backgroundMusic;
        m_audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        m_audioSource.Stop();
    }
}
