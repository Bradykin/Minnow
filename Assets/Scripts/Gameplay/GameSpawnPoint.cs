using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPointType : int
{
    Random,
    UnitType
}

public class GameSpawnPoint : GameElementBase, ISave, ILoad<JsonGameSpawnPointData>
{
    public GameTile m_tile;
    public SpawnPointType m_gameSpawnPointType;
    public string m_gameSpawnUnitName;

    public GameSpawnPoint()
    {
        m_gameSpawnPointType = SpawnPointType.Random;
        m_gameSpawnUnitName = string.Empty;
    }

    public void SetSpawnPointRandom()
    {
        m_gameSpawnPointType = SpawnPointType.Random;
        m_gameSpawnUnitName = string.Empty;
    }

    public void SetSpawnPointUnit(string gameSpawnUnitName)
    {
        m_gameSpawnPointType = SpawnPointType.UnitType;
        m_gameSpawnUnitName = gameSpawnUnitName;
    }

    public void SetSpawnPointUnit(GameUnit gameSpawnUnit)
    {
        m_gameSpawnPointType = SpawnPointType.UnitType;
        m_gameSpawnUnitName = gameSpawnUnit.m_name;
    }

    //============================================================================================================//

    public string SaveToJsonAsString()
    {
        JsonGameSpawnPointData jsonData = new JsonGameSpawnPointData
        {
            gameSpawnPointType = (int)m_gameSpawnPointType,
            gameSpawnUnitName = m_gameSpawnUnitName
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameSpawnPointData jsonData)
    {
        if (jsonData.gameSpawnPointType == (int)SpawnPointType.Random)
        {
            SetSpawnPointRandom();
        }
        else if (jsonData.gameSpawnPointType == (int)SpawnPointType.UnitType)
        {
            SetSpawnPointUnit(jsonData.gameSpawnUnitName);
        }
    }
}
