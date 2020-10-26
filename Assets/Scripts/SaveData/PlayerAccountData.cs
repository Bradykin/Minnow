using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAccountData
{
    //Key = map ID, value = Chaos progression (highest chaos BEATEN)
    public Dictionary<int, int> m_mapChaosLevels;

    public int m_playerExperience;
    public int m_numPlaySessions;

    public PlayerAccountData()
    {
        m_mapChaosLevels = new Dictionary<int, int>();
    }
}
