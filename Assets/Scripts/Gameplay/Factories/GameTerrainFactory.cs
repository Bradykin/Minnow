using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTerrainFactory
{
    private static List<GameTerrainBase> m_terrain = new List<GameTerrainBase>();

    private static bool m_hasInit = false;
    
    public static void Init()
    {
        m_terrain.Add(new ContentForestTerrain());
        m_terrain.Add(new ContentGrassTerrain());
        m_terrain.Add(new ContentMountainTerrain());
        m_terrain.Add(new ContentWaterTerrain());
        m_terrain.Add(new ContentTundraForestTerrain());
        m_terrain.Add(new ContentSnowForestTerrain());
        m_terrain.Add(new ContentJungleForestTerrain());
        m_terrain.Add(new ContentSwampForestTerrain());
        m_terrain.Add(new ContentDesertRedForestTerrain());
        m_hasInit = true;
    }
    
    public static GameTerrainBase GetRandomTerrain()
    {
        if (!m_hasInit)
            Init();
        
        int r = UnityEngine.Random.Range(0, m_terrain.Count);

        return (GameTerrainBase)Activator.CreateInstance(m_terrain[r].GetType());
    }

    public static GameTerrainBase GetTerrainClone(GameTerrainBase currentTerrain)
    {
        return (GameTerrainBase)Activator.CreateInstance(currentTerrain.GetType());
    }

    public static GameTerrainBase GetNextTerrain(GameTerrainBase currentTerrain)
    {
        if (!m_hasInit)
            Init();

        int r = m_terrain.FindIndex(t => t.GetType() == currentTerrain.GetType());

        if (r == m_terrain.Count - 1)
            r = 1;
        else
            r++;

        return (GameTerrainBase)Activator.CreateInstance(m_terrain[r].GetType());
    }

    public static GameTerrainBase GetTerrainFromJson(JsonGameTerrainData jsonData)
    {
        if (!m_hasInit)
            Init();

        int i = m_terrain.FindIndex(t => t.m_name == jsonData.name);

        GameTerrainBase newTerrain = (GameTerrainBase)Activator.CreateInstance(m_terrain[i].GetType());
        newTerrain.LoadFromJson(jsonData);

        return newTerrain;
    }
}