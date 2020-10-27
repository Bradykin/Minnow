using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Globals
{
    public enum ChaosLevels : int
    {
        AddCards = 1,
        MapEvents = 2,
        EnemyStrength = 3,
        AddEnemyAbility = 4,
        BossStrength = 5
    }

    //TODO: THis is temp.
    public static bool loadingRun = false;
    public static string mapToLoad = string.Empty;

    public static UICard m_selectedCard;
    public static WorldUnit m_selectedUnit;
    public static WorldTile m_selectedTile;
    public static WorldUnit m_selectedEnemy;
    public static WorldTile m_hoveredTile;
    public static UICard m_hoveredCard;

    public static GameBuildingIntermission m_selectedIntermissionBuilding;
    public static bool m_canSelect = true;
    public static bool m_canScroll = true;
    public static bool m_inIntermission = false;

    //Sizing for a "square" hexagon grid
    public static int GridSizeX = 30;
    public static int GridSizeY = 30;

    //Values that should be in some "LevelCreatorState" data
    public static bool m_levelCreatorEraserMode = false;
    public static Type m_currentlyPaintingType;
    public static GameBuildingBase m_currentlyPaintingBuilding;
    public static GameTerrainBase m_currentlyPaintingTerrain;
    public static int m_currentlyPaintingNumberIndex;

    //Moved some some UI thing? IDK.
    public static bool m_inDeckView = false;

    //Temp testing value
    public static GameEnemyUnit m_testSpawnEnemyUnit = null;
    public static GameEnemyUnit m_focusedDebugEnemyUnit = null;

    //Values that will be moved into player save data
    public static int m_spellsPlayedPreviousTurn = 0;
    public static int m_spellsPlayedThisTurn = 0;
    public static int m_fletchingCount = 0;
    public static int m_totemOfTheWolfTurn = -1;
    public static int m_goldPerShivKill = 0;
    public static int m_curChaos = 1;
    public static int m_tempSpellpower = 0;
    public static bool m_worthySacrificeEvent = false;

    private static bool m_hasInit;
    public static bool m_levelActive;

    public static void Init()
    {
        m_hasInit = true;
    }

    public static List<JsonMapMetaData> LoadMapMetaData()
    {
        if (!m_hasInit)
            Init();

#if UNITY_EDITOR
        string path = Path.Combine(Files.EDITOR_PATH, Files.MAP_META_DATA_PATH);
#else
        string path = Path.Combine(Files.BUILD_PATH, Files.MAP_META_DATA_PATH);
#endif

        if (!File.Exists(path))
        {
            return new List<JsonMapMetaData>();
        }
        JsonMapFilesMetaData jsonMapFilesMetaData = JsonConvert.DeserializeObject<JsonMapFilesMetaData>(File.ReadAllText(path));

        List<JsonMapMetaData> jsonMapMetaDatas = new List<JsonMapMetaData>();
        for (int i = 0; i < jsonMapFilesMetaData.mapFiles.Count; i++)
        {
            jsonMapMetaDatas.Add(JsonConvert.DeserializeObject<JsonMapMetaData>(jsonMapFilesMetaData.mapFiles[i]));
        }

        return jsonMapMetaDatas;
    }

    public static void SaveMapMetaData(List<JsonMapMetaData> jsonMapMetaData)
    {
        if (!m_hasInit)
            Init();

#if UNITY_EDITOR
        string path = Path.Combine(Files.EDITOR_PATH, Files.MAP_META_DATA_PATH);
#else
        string path = Path.Combine(Files.BUILD_PATH, Files.MAP_META_DATA_PATH);
#endif

        jsonMapMetaData = jsonMapMetaData.OrderBy(j => j.mapID).ToList();

        JsonMapFilesMetaData jsonMapFilesMetaData = new JsonMapFilesMetaData
        {
            mapFiles = new List<string>()
        };

        for (int i = 0; i < jsonMapMetaData.Count; i++)
        {
            jsonMapFilesMetaData.mapFiles.Add(JsonConvert.SerializeObject(jsonMapMetaData[i]));
        }

        string jsonData = JsonConvert.SerializeObject(jsonMapFilesMetaData);
        File.WriteAllText(path, jsonData);
    }
}
