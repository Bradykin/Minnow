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
        if (GlobalSettings.m_music)
        {
            m_audioSource.volume = 1.0f;
        }
        else
        {
            m_audioSource.volume = 0.0f;
        }
    }

    public void StartBackgroundMusic(GameMap toStart)
    {
        m_audioSource.clip = toStart.m_backgroundMusic;
        m_audioSource.Play();
    }
}
