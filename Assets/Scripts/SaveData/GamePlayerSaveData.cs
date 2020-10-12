using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerSaveData
{
    //Key = map ID, value = Chaos progression (highest chaos BEATEN)
    public Dictionary<int, int> m_mapChaosLevels;

    public int m_playerExperience;
    public int m_numPlaySessions;

    public GamePlayerSaveData()
    {
        m_mapChaosLevels = new Dictionary<int, int>();
    }
}
