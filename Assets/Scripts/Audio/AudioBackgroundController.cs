using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Util;

public class AudioBackgroundController : Singleton<AudioBackgroundController>
{
    private AudioSource m_audioSource;

    public bool m_playingMenuMusic;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        StartMenuMusic();
    }

    void Update()
    {
        m_audioSource.volume = PlayerDataManager.PlayerAccountData.m_musicVolume;

        if (!UIHelper.LevelSelectHasMap() && !m_playingMenuMusic)
        {
            StartMenuMusic();
        }
    }

    public void StartBackgroundMusic(GameMap toStart)
    {
        m_audioSource.Stop();

        m_audioSource.clip = toStart.m_backgroundMusic;
        m_audioSource.Play();

        m_playingMenuMusic = false;
    }

    public void StopBackgroundMusic()
    {
        m_audioSource.Stop();
    }

    private void StartMenuMusic()
    {
        m_audioSource.Stop();

        m_audioSource.clip = AudioHelper.MenuBackgroundMusic;
        m_audioSource.Play();

        m_playingMenuMusic = true;
    }
}
