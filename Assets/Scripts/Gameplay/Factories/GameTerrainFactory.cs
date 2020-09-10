using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GameTerrainFactory
{
    private static List<KeyValuePair<string, List<GameTerrainBase>>> m_terrain = new List<KeyValuePair<string, List<GameTerrainBase>>>();
    private static int m_currentTerrainListIndex;
    private static int m_currentTerrainIndex;

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
    private static List<GameTerrainBase> m_ruinsTerrain = new List<GameTerrainBase>();

    private static bool m_hasInit = false;
    
    public static void Init()
    {
        //Basic Terrain
        m_basicTerrain.Add(new ContentScrublandPlainsTerrain());
        m_basicTerrain.Add(new ContentGrassPlainsTerrain());
        m_basicTerrain.Add(new ContentDirtPlainsTerrain());
        m_basicTerrain.Add(new ContentForestTerrain());
        m_basicTerrain.Add(new ContentHillsTerrain());
        m_basicTerrain.Add(new ContentMountainTerrain());
        m_basicTerrain.Add(new ContentWaterTerrain());
        m_basicTerrain.Add(new ContentGrassPlainsRuinsTerrain());
        m_basicTerrain.Add(new ContentForestRuinsTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Basic Terrain", m_basicTerrain));

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
        m_plainsTerrain.Add(new ContentTropicalSandPlainsTerrain());
        m_plainsTerrain.Add(new ContentTropicalGrassSandPlainsTerrain());
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
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Plains Terrain", m_plainsTerrain));

        //Forest Terrain
        m_forestTerrain.Add(new ContentForestTerrain());
        m_forestTerrain.Add(new ContentWoodlandsForestTerrain());
        m_forestTerrain.Add(new ContentPineForestTerrain());
        m_forestTerrain.Add(new ContentTundraForestTerrain());
        m_forestTerrain.Add(new ContentSnowForestTerrain());
        m_forestTerrain.Add(new ContentSwampForestTerrain());
        m_forestTerrain.Add(new ContentJungleForestTerrain());
        m_forestTerrain.Add(new ContentTropicalSandForestTerrain());
        m_forestTerrain.Add(new ContentTropicalGrassSandForestTerrain());
        m_forestTerrain.Add(new ContentDesertRedForestTerrain());
        m_forestTerrain.Add(new ContentDesertRedForestPondTerrain());
        m_forestTerrain.Add(new ContentDesertYellowForestTerrain());
        m_forestTerrain.Add(new ContentDirtForestBurnedTerrain());
        m_forestTerrain.Add(new ContentAshForestBurnedTerrain());
        m_forestTerrain.Add(new ContentForestRuinsTerrain());
        m_forestTerrain.Add(new ContentSnowForestRuinsTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Forest Terrain", m_forestTerrain));

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
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Hills Terrain", m_hillTerrain));

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
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Mountain Terrain", m_mountainTerrain));

        //Tropical Terrain
        m_tropicalTerrain.Add(new ContentTropicalPlainsTerrain());
        m_tropicalTerrain.Add(new ContentTropicalSandPlainsTerrain());
        m_tropicalTerrain.Add(new ContentTropicalGrassSandPlainsTerrain());
        m_tropicalTerrain.Add(new ContentTropicalSandForestTerrain());
        m_tropicalTerrain.Add(new ContentTropicalGrassSandForestTerrain());
        m_tropicalTerrain.Add(new ContentWetlandsPlainsTerrain());
        m_tropicalTerrain.Add(new ContentSwampForestTerrain());
        m_tropicalTerrain.Add(new ContentBogTerrain());
        m_tropicalTerrain.Add(new ContentMarshTerrain());
        m_tropicalTerrain.Add(new ContentMarshRuinsTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Tropical Terrain", m_tropicalTerrain));

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
        m_snowTerrain.Add(new ContentSnowRuinsTerrain());
        m_snowTerrain.Add(new ContentIceWaterTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Snow Terrain", m_snowTerrain));

        //Desert Red Terrain
        m_redDesertTerrain.Add(new ContentDesertRedDirtPlainsTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedGrassPlainsTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedGrassDunesTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedForestTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedHillsTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedMountainTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedMesaLargeTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedMountainCaveTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedMesaLargeCaveTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedGrassPlainsPondTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedForestPondTerrain());
        m_redDesertTerrain.Add(new ContentDesertRedHillsPondTerrain());
        m_redDesertTerrain.Add(new ContentDesertDunesTerrain());
        m_redDesertTerrain.Add(new ContentDesertRuinsTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Red Desert Terrain", m_redDesertTerrain));

        //Desert Yellow Terrain
        m_yellowDesertTerrain.Add(new ContentDesertYellowDirtPlainsTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowForestTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowHillsTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowMesaTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowMesaLargeTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowDirtDunesTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowCraterTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowSaltFlatsTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowMesaCaveTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowMesaLargeCaveTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowHillsPondTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertYellowMesaLargePondTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertDunesTerrain());
        m_yellowDesertTerrain.Add(new ContentDesertRuinsTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Yellow Desert Terrain", m_yellowDesertTerrain));

        //Volcanic Terrain
        m_volcanicTerrain.Add(new ContentAshPlainsTerrain());
        m_volcanicTerrain.Add(new ContentAshForestBurnedTerrain());
        m_volcanicTerrain.Add(new ContentDirtForestBurnedTerrain());
        m_volcanicTerrain.Add(new ContentLavaFieldActiveTerrain());
        m_volcanicTerrain.Add(new ContentLavaFieldInactiveTerrain());
        m_volcanicTerrain.Add(new ContentVolcanoActiveTerrain());
        m_volcanicTerrain.Add(new ContentVolcanoInactiveTerrain());
        m_volcanicTerrain.Add(new ContentFumarolePlainsTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Volcanic Terrain", m_volcanicTerrain));

        //Ruins Terrain
        m_ruinsTerrain.Add(new ContentGrassPlainsRuinsTerrain());
        m_ruinsTerrain.Add(new ContentDirtPlainsRuinsTerrain());
        m_ruinsTerrain.Add(new ContentForestRuinsTerrain());
        m_ruinsTerrain.Add(new ContentPineForestRuinsTerrain());
        m_ruinsTerrain.Add(new ContentSnowForestRuinsTerrain());
        m_ruinsTerrain.Add(new ContentMarshRuinsTerrain());
        m_ruinsTerrain.Add(new ContentDesertRuinsTerrain());
        m_ruinsTerrain.Add(new ContentSnowRuinsTerrain());
        m_terrain.Add(new KeyValuePair<string, List<GameTerrainBase>>("Ruins Terrain", m_ruinsTerrain));

        m_currentTerrainIndex = 0;
        m_currentTerrainListIndex = 0;
        m_hasInit = true;
    }

    public static GameTerrainBase GetCurrentTerrain()
    {
        if (!m_hasInit)
            Init();

        return GetTerrainClone(m_terrain[m_currentTerrainListIndex].Value[m_currentTerrainIndex]);
    }

    public static List<GameTerrainBase> GetCurrentTerrainList()
    {
        if (!m_hasInit)
            Init();

        return m_terrain[m_currentTerrainListIndex].Value;
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

    public static GameTerrainBase GetNextTerrainList()
    {
        if (!m_hasInit)
            Init();

        if (m_currentTerrainListIndex == m_terrain.Count - 1)
            m_currentTerrainListIndex = 0;
        else
            m_currentTerrainListIndex++;

        m_currentTerrainIndex = 0;

        return m_terrain[m_currentTerrainListIndex].Value[m_currentTerrainIndex];
    }

    public static GameTerrainBase GetNextTerrain()
    {
        if (!m_hasInit)
            Init();

        if (m_currentTerrainIndex == m_terrain[m_currentTerrainListIndex].Value.Count - 1)
            m_currentTerrainIndex = 0;
        else
            m_currentTerrainIndex++;


        return (GameTerrainBase)Activator.CreateInstance(m_terrain[m_currentTerrainListIndex].Value[m_currentTerrainIndex].GetType());
    }

    public static GameTerrainBase GetTerrainFromJson(JsonGameTerrainData jsonData)
    {
        if (!m_hasInit)
            Init();

        for (int i = 0; i < m_terrain.Count; i++)
        {
            List<GameTerrainBase> currentTerrainList = m_terrain[i].Value;

            int r = currentTerrainList.FindIndex(t => t.m_name == jsonData.name);

            if (r == -1)
            {
                continue;
            }

            GameTerrainBase newTerrain = (GameTerrainBase)Activator.CreateInstance(currentTerrainList[r].GetType());
            newTerrain.LoadFromJson(jsonData);

            return newTerrain;
        }

        Debug.LogError("Missing terrain class for " + jsonData.name + " in GameTerrainFactory");
        return null;
    }
}