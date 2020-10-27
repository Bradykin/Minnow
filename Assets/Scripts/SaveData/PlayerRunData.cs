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

    public int m_mapId;

    public JsonMapData m_jsonMapData;
    
    public PlayerRunData()
    {

    }

    public void SaveRunData()
    {
        GameController gameController = GameHelper.GetGameController();
        if (gameController == null)
        {
            Debug.LogError("Trying to save player run data, can't find gamecontroller");
        }

        m_mapId = gameController.GetCurMap().m_id;
        
        m_jsonMapData = WorldGridManager.Instance.SaveToJson();
    }
}
