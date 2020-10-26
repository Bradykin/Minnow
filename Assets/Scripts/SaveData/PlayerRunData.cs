using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRunData
{
    public int m_currentWave;
    public int m_currentTurn;

    public JsonGridData m_jsonGridData;
    
    public PlayerRunData()
    {
        
    }

    public void SaveRunData()
    {
        m_jsonGridData = WorldGridManager.Instance.SaveToJson();
    }
}
