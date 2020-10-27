using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawnPoint : GameElementBase, ISave<JsonGameSpawnPointData>, ILoad<JsonGameSpawnPointData>
{
    public GameTile m_tile;
    public List<int> m_spawnPointMarkers;

    public GameSpawnPoint()
    {
        m_spawnPointMarkers = new List<int>();
    }

    //============================================================================================================//

    public JsonGameSpawnPointData SaveToJson()
    {
        JsonGameSpawnPointData jsonData = new JsonGameSpawnPointData
        {
            gameSpawnPointMarkers = m_spawnPointMarkers
        };

        return jsonData;
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
