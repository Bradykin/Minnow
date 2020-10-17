﻿using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnPoint : GameElementBase, ISave, ILoad<JsonGameSpawnPointData>
{
    public GameTile m_tile;
    public List<int> m_spawnPointMarkers;

    public GameSpawnPoint()
    {
        m_spawnPointMarkers = new List<int>();
    }

    //============================================================================================================//

    public string SaveToJsonAsString()
    {
        JsonGameSpawnPointData jsonData = new JsonGameSpawnPointData
        {
            gameSpawnPointMarkers = m_spawnPointMarkers
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameSpawnPointData jsonData)
    {
        if (jsonData.gameSpawnPointMarkers != null)
        {
            m_spawnPointMarkers = jsonData.gameSpawnPointMarkers;
        }
        else
        {
            m_spawnPointMarkers = new List<int>();
        }
    }
}
