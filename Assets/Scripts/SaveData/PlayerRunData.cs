using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerRunData
{
    public JsonGameControllerData m_jsonGameControllerData;
    public JsonMapData m_jsonMapData;
    
    public PlayerRunData()
    {

    }

    public void SaveRunData()
    {
        if (!WorldController.Instance.m_isInGame)
        {
            Debug.LogError("Tried to save run while not in game");
            return;
        }
        
        GameController gameController = GameHelper.GetGameController();
        if (gameController == null)
        {
            Debug.LogError("Trying to save player run data, can't find gamecontroller");
        }

        m_jsonGameControllerData = gameController.SaveToJson();
        
        m_jsonMapData = WorldGridManager.Instance.SaveToJson();
    }
}
