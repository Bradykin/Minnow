using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTerrainFactory
{
    private static List<GameTerrainBase> m_terrain = new List<GameTerrainBase>();

    private static bool hasInit = false;
    
    public static void Init()
    {
        m_terrain.Add(new ContentForestTerrain());
        m_terrain.Add(new ContentGrassTerrain());
        m_terrain.Add(new ContentMountainTerrain());
        m_terrain.Add(new ContentWaterTerrain());
        hasInit = true;
    }
    
    public static GameTerrainBase GetRandomTerrain()
    {
        if (!hasInit)
            Init();
        
        int r = UnityEngine.Random.Range(0, m_terrain.Count);

        return (GameTerrainBase)Activator.CreateInstance(m_terrain[r].GetType());
    }

    public static GameTerrainBase GetNextTerrain(GameTerrainBase currentTerrain)
    {
        if (!hasInit)
            Init();

        int r = m_terrain.FindIndex(t => t.GetType() == currentTerrain.GetType());

        if (r == m_terrain.Count - 1)
            r = 1;
        else
            r++;

        return (GameTerrainBase)Activator.CreateInstance(m_terrain[r].GetType());
    }
}