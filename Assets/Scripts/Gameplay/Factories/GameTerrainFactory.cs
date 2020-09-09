using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTerrainFactory
{
    private static List<GameTerrainBase> m_terrain = new List<GameTerrainBase>();

    private static List<GameTerrainBase> m_basicTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_plainsTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_forestTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_hillTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_mountainTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_tropicalTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_snowTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_redDesertTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_yellowDesertTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_volcanicTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_pondTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_caveTerrain = new List<GameTerrainBase>();
    private static List<GameTerrainBase> m_ruinsTerrain = new List<GameTerrainBase>();

    private static bool m_hasInit = false;
    
    public static void Init()
    {
        //Basic Terrain
        m_basicTerrain.Add(new ContentScrublandPlainsTerrain());
        m_basicTerrain.Add(new ContentDirtPlainsTerrain());
        m_basicTerrain.Add(new ContentForestTerrain());
        m_basicTerrain.Add(new ContentHillsTerrain());
        m_basicTerrain.Add(new ContentMountainTerrain());
        m_basicTerrain.Add(new ContentWaterTerrain());
        m_basicTerrain.Add(new ContentGrassPlainsRuinsTerrain());
        m_basicTerrain.Add(new ContentForestRuinsTerrain());

        //Plains Terrain
        m_plainsTerrain.Add(new ContentScrublandPlainsTerrain());
        m_plainsTerrain.Add(new ContentGrassPlainsTerrain());
        m_plainsTerrain.Add(new ContentDirtPlainsTerrain());
        m_plainsTerrain.Add(new ContentHighlandsPlainsTerrain());
        m_plainsTerrain.Add(new ContentColdPlainsTerrain());
        m_plainsTerrain.Add(new ContentColdDirtPlainsTerrain());
        m_plainsTerrain.Add(new ContentTundraPlainsTerrain());
        m_plainsTerrain.Add(new ContentSnowPlainsTerrain());
        m_plainsTerrain.Add(new ContentTropicalPlainsTerrain());
        m_plainsTerrain.Add(new ContentSandTropicalPlainsTerrain());
        m_plainsTerrain.Add(new ContentGrassSandTropicalPlainsTerrain());
        m_plainsTerrain.Add(new ContentWetlandsPlainsTerrain());
        m_plainsTerrain.Add(new ContentDesertRedDirtPlainsTerrain());
        m_plainsTerrain.Add(new ContentDesertRedGrassPlainsTerrain());
        m_plainsTerrain.Add(new ContentDesertYellowDirtPlainsTerrain());
        m_plainsTerrain.Add(new ContentFumarolePlainsTerrain());
        m_plainsTerrain.Add(new ContentAshPlainsTerrain());
        m_plainsTerrain.Add(new ContentColdPlainsPondTerrain());
        m_plainsTerrain.Add(new ContentTundraPlainsPondTerrain());
        m_plainsTerrain.Add(new ContentSnowPlainsPondTerrain());
        m_plainsTerrain.Add(new ContentDesertRedGrassPlainsPondTerrain());

        //Forest Terrain
        m_forestTerrain.Add(new ContentForestTerrain());
        m_forestTerrain.Add(new ContentWoodlandsForestTerrain());
        m_forestTerrain.Add(new ContentPineForestTerrain());
        m_forestTerrain.Add(new ContentTundraForestTerrain());
        m_forestTerrain.Add(new ContentSnowForestTerrain());
        m_forestTerrain.Add(new ContentSwampForestTerrain());
        m_forestTerrain.Add(new ContentJungleForestTerrain());
        m_forestTerrain.Add(new ContentSandTropicalForestTerrain());
        m_forestTerrain.Add(new ContentGrassSandTropicalForestTerrain());
        m_forestTerrain.Add(new ContentDesertRedForestTerrain());
        m_forestTerrain.Add(new ContentDesertRedForestPondTerrain());
        m_forestTerrain.Add(new ContentDesertYellowForestTerrain());
        m_forestTerrain.Add(new ContentDirtForestBurnedTerrain());
        m_forestTerrain.Add(new ContentAshForestBurnedTerrain());
        m_forestTerrain.Add(new ContentForestRuinsTerrain());
        m_forestTerrain.Add(new ContentSnowForestRuinsTerrain());

        //Hill Terrain
        m_hillTerrain.Add(new ContentHillsTerrain());
        m_hillTerrain.Add(new ContentColdHillsTerrain());
        m_hillTerrain.Add(new ContentTundraHillsTerrain());
        m_hillTerrain.Add(new ContentSnowHillsTerrain());
        m_hillTerrain.Add(new ContentDesertRedHillsTerrain());
        m_hillTerrain.Add(new ContentDesertYellowHillsTerrain());
        m_hillTerrain.Add(new ContentDesertYellowMesaTerrain());
        m_hillTerrain.Add(new ContentColdHillsCaveTerrain());
        m_hillTerrain.Add(new ContentTundraHillsCaveTerrain());
        m_hillTerrain.Add(new ContentSnowHillsCaveTerrain());
        m_hillTerrain.Add(new ContentDesertYellowMesaCaveTerrain());
        m_hillTerrain.Add(new ContentDesertRedHillsPondTerrain());
        m_hillTerrain.Add(new ContentDesertYellowHillsPondTerrain());

        //Mountain Terrain
        m_mountainTerrain.Add(new ContentMountainTerrain());
        m_mountainTerrain.Add(new ContentDesertRedMountainTerrain());
        m_mountainTerrain.Add(new ContentDesertRedMesaLargeTerrain());
        m_mountainTerrain.Add(new ContentDesertYellowMesaLargeTerrain());
        m_mountainTerrain.Add(new ContentSnowMountainTerrain());
        m_mountainTerrain.Add(new ContentSnowMountainCaveTerrain());
        m_mountainTerrain.Add(new ContentDesertRedMountainCaveTerrain());
        m_mountainTerrain.Add(new ContentDesertRedMesaLargeCaveTerrain());
        m_mountainTerrain.Add(new ContentDesertYellowMesaLargeCaveTerrain());
        m_mountainTerrain.Add(new ContentDesertYellowMesaLargePondTerrain());
        m_mountainTerrain.Add(new ContentVolcanoActiveTerrain());
        m_mountainTerrain.Add(new ContentVolcanoInactiveTerrain());

        //Snow Terrain
        m_snowTerrain.Add(new ContentSnowPlainsTerrain());
        m_snowTerrain.Add(new ContentSnowForestTerrain());
        m_snowTerrain.Add(new ContentSnowHillsTerrain());
        m_snowTerrain.Add(new ContentSnowMountainTerrain());
        m_snowTerrain.Add(new ContentSnowBankTerrain());
        m_snowTerrain.Add(new ContentSnowHillsCaveTerrain());
        m_snowTerrain.Add(new ContentSnowMountainCaveTerrain());
        m_snowTerrain.Add(new ContentSnowPlainsPondTerrain());
        m_snowTerrain.Add(new ContentSnowForestRuinsTerrain());
        m_snowTerrain.Add(new ContentTundraPlainsTerrain());
        m_snowTerrain.Add(new ContentTundraForestTerrain());
        m_snowTerrain.Add(new ContentTundraHillsTerrain());
        m_snowTerrain.Add(new ContentTundraHillsCaveTerrain());
        m_snowTerrain.Add(new ContentTundraPlainsPondTerrain());
        m_snowTerrain.Add(new ContentColdPlainsTerrain());
        m_snowTerrain.Add(new ContentColdDirtPlainsTerrain());
        m_snowTerrain.Add(new ContentColdHillsTerrain());
        m_snowTerrain.Add(new ContentColdHillsCaveTerrain());
        m_snowTerrain.Add(new ContentColdPlainsPondTerrain());

        //Ruins Terrain
        m_ruinsTerrain.Add(new ContentGrassPlainsRuinsTerrain());
        m_ruinsTerrain.Add(new ContentDirtPlainsRuinsTerrain());
        m_ruinsTerrain.Add(new ContentForestRuinsTerrain());
        m_ruinsTerrain.Add(new ContentPineForestRuinsTerrain());
        m_ruinsTerrain.Add(new ContentSnowForestRuinsTerrain());
        m_ruinsTerrain.Add(new ContentSwampForestRuinsTerrain());
        m_ruinsTerrain.Add(new ContentDesertRuinsTerrain());
        m_ruinsTerrain.Add(new ContentMarshRuinsTerrain());

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