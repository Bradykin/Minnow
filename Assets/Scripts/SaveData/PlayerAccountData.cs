using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAccountData
{
    public PlayerRunData PlayerRunData
    {
        get
        {
            if (m_playerRunData == null)
            {
                m_playerRunData = Files.ImportPlayerRunData();
            }
            return m_playerRunData;
        }
        set
        {
            m_playerRunData = value;
        }
    }
    private PlayerRunData m_playerRunData;

    [JsonIgnore]
    public List<JsonGameMetaProgressionRewardData> JsonGameMetaProgressionRewardDatas => m_jsonGameMetaProgressionRewardDatas;
    [JsonProperty]
    private List<JsonGameMetaProgressionRewardData> m_jsonGameMetaProgressionRewardDatas;
    
    //Key = map ID, value = Chaos progression (highest chaos BEATEN)
    public Dictionary<int, int> m_mapChaosLevels;

    //Key = map ID, value = most recent chaos hovered on this map in menu
    public Dictionary<int, int> m_mapChaosUIAutoset;

    public Dictionary<string, int> m_starterCardUnlockLevels;
    public Dictionary<string, int> m_starterRelicUnlockLevels;

    public string StarterSimpleUnitName = new ContentDwarvenSoldier().GetBaseName();
    public string StarterAdvancedUnitName = new ContentAlphaBoar().GetBaseName();
    public string StarterDamageSpellName = new ContentFireboltCard().GetBaseName();
    public string StarterDefensiveSpellName = new ContentAegisCard().GetBaseName();
    public string StarterExileSpellName = new ContentGrowTalonsCard().GetBaseName();
    public string StarterRelicName = new ContentMaskOfAgesRelic().GetBaseName();

    public int m_playerExperience;
    public int m_numPlaySessions;

    public bool m_altarsUnlockedOnAccount;

    public float m_musicVolume = 0; // AudioHelper.DefaultMusicVolume; //0.0f - 1.0f
    public float m_sfxVolume = 0;// AudioHelper.DefaultSFXVolume; //0.0f - 1.0f
    public bool m_followEnemy = true;

    public PlayerAccountData()
    {
        m_mapChaosLevels = new Dictionary<int, int>();
        m_mapChaosUIAutoset = new Dictionary<int, int>();
        m_starterCardUnlockLevels = new Dictionary<string, int>();
        m_starterRelicUnlockLevels = new Dictionary<string, int>();
    }

    public bool HasPreviouslyBeatenMapChaosLevel(int mapID, int chaos)
    {
        if (!m_mapChaosLevels.ContainsKey(mapID))
        {
            return false;
        }
        else if (m_mapChaosLevels[mapID] < chaos)
        {
            return false;
        }

        return true;
    }

    public void SaveGameMetaProgressionRewardDatas()
    {
        m_jsonGameMetaProgressionRewardDatas = new List<JsonGameMetaProgressionRewardData>();
        for (int i = 0; i < UIMetaprogressionNotificationController.GetRewards().Count; i++)
        {
            m_jsonGameMetaProgressionRewardDatas.Add(UIMetaprogressionNotificationController.GetRewards()[i].SaveToJson());
        }
    }

    public void SaveRunData()
    {
        m_playerRunData = new PlayerRunData();
        m_playerRunData.SaveRunData();
    }

    public void ClearRunData()
    {
        m_playerRunData = null;
    }
}
