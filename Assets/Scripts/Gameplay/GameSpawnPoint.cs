using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnPointType : int
{
    Random,
    EntityType
}

public class GameSpawnPoint : GameElementBase, ISave, ILoad<JsonGameSpawnPointData>
{
    public GameTile m_tile;
    public SpawnPointType m_gameSpawnPointType;
    public string m_gameSpawnEntityName;

    public GameSpawnPoint()
    {
        m_gameSpawnPointType = SpawnPointType.Random;
        m_gameSpawnEntityName = string.Empty;
    }

    public void SetSpawnPointRandom()
    {
        m_gameSpawnPointType = SpawnPointType.Random;
        m_gameSpawnEntityName = string.Empty;
    }

    public void SetSpawnPointEntity(string gameSpawnEntityName)
    {
        m_gameSpawnPointType = SpawnPointType.EntityType;
        m_gameSpawnEntityName = gameSpawnEntityName;
    }

    public void SetSpawnPointEntity(GameEntity gameSpawnEntity)
    {
        m_gameSpawnPointType = SpawnPointType.EntityType;
        m_gameSpawnEntityName = gameSpawnEntity.m_name;
    }

    //============================================================================================================//

    public string SaveToJson()
    {
        JsonGameSpawnPointData jsonData = new JsonGameSpawnPointData
        {
            gameSpawnPointType = (int)m_gameSpawnPointType,
            gameSpawnEntityName = m_gameSpawnEntityName
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public void LoadFromJson(JsonGameSpawnPointData jsonData)
    {
        if (jsonData.gameSpawnPointType == (int)SpawnPointType.Random)
        {
            SetSpawnPointRandom();
        }
        else if (jsonData.gameSpawnPointType == (int)SpawnPointType.EntityType)
        {
            SetSpawnPointEntity(jsonData.gameSpawnEntityName);
        }
    }
}
