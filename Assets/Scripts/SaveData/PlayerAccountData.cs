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

    public int m_playerExperience;
    public int m_numPlaySessions;

    public PlayerAccountData()
    {
        m_mapChaosLevels = new Dictionary<int, int>();
    }

    public void SaveRunData()
    {
        m_playerRunData = new PlayerRunData();
        m_playerRunData.SaveRunData();
    }
}
