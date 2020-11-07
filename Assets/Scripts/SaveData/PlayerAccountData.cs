using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAccountData
{
    [JsonIgnore]
    public PlayerRunData PlayerRunData => m_playerRunData;
    [JsonProperty]
    private PlayerRunData m_playerRunData = null;
    
    //Key = map ID, value = Chaos progression (highest chaos BEATEN)
    public Dictionary<int, int> m_mapChaosLevels;

    //Key = map ID, value = most recent chaos hovered on this map in menu
    public Dictionary<int, int> m_mapChaosUIAutoset;

    public Dictionary<string, int> m_starterCardUnlockLevels;

    public string StarterSimpleUnitName = "Dwarven Soldier";
    public string StarterAdvancedUnitName = "Alpha Boar";
    public string StarterDamageSpellName = "Firebolt";
    public string StarterDefensiveSpellName = "Aegis";
    public string StarterExileSpellName = "Grow Talons";
    public string StarterRelicName = "Mask of Ages";

    public int m_playerExperience;
    public int m_numPlaySessions;

    public float m_musicVolume = 0; // AudioHelper.DefaultMusicVolume; //0.0f - 1.0f
    public float m_sfxVolume = 0;// AudioHelper.DefaultSFXVolume; //0.0f - 1.0f
    public bool m_followEnemy = false;

    public PlayerAccountData()
    {
        m_mapChaosLevels = new Dictionary<int, int>();
        m_mapChaosUIAutoset = new Dictionary<int, int>();
        m_starterCardUnlockLevels = new Dictionary<string, int>();
    }

    public void SaveRunData()
    {
        m_playerRunData = new PlayerRunData();
        m_playerRunData.SaveRunData();
    }
}
