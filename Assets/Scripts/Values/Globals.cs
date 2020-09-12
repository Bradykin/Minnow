using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class Globals
{
    //TODO: THis is temp. This datapath should be gotten from the menu before loading the level
    private static string applicationDataPath;
    private static string mapMetaDataPath = "/RemoteData/SaveMetaData.txt";
    private static string defaultGridDataPath = "/RemoteData/JsonGridData0.txt";
    public static string mapToLoad = string.Empty;

    public static UICard m_selectedCard;
    public static UIEntity m_selectedEntity;
    public static GameBuildingIntermission m_selectedIntermissionBuilding;
    public static bool m_canSelect = true;
    public static bool m_canScroll = true;
    public static bool m_inIntermission = false;

    //Sizing for a "square" hexagon grid
    public static int GridSizeX = 30;
    public static int GridSizeY = 30;

    public static bool m_levelCreatorEraserMode = false;
    public static Type m_currentlyPaintingType;
    public static GameBuildingBase m_currentlyPaintingBuilding;
    public static GameTerrainBase m_currentlyPaintingTerrain;
    public static ContentAngelicGiftEvent m_currentlyPaintingEvent;
    public static bool m_inDeckView = false;

    public static int m_purpleBeamCount = 0;

    public static int m_curChaos = 0;

    private static bool m_hasInit;

    public static void Init()
    {
        applicationDataPath = Application.dataPath;

        m_hasInit = true;
    }

    public static string GetMapMetaDataPath()
    {
        if (!m_hasInit)
            Init();

        return applicationDataPath + mapMetaDataPath;
    }

    public static string GetDefaultGridDataPath()
    {
        if (!m_hasInit)
            Init();

        return applicationDataPath + defaultGridDataPath;
    }

    public static List<JsonMapMetaData> LoadMapMetaData()
    {
        if (!m_hasInit)
            Init();
        
        if (!File.Exists(applicationDataPath + mapMetaDataPath))
        {
            return new List<JsonMapMetaData>();
        }
        JsonMapFilesMetaData jsonMapFilesMetaData = JsonUtility.FromJson<JsonMapFilesMetaData>(File.ReadAllText(applicationDataPath + mapMetaDataPath));

        List<JsonMapMetaData> jsonMapMetaDatas = new List<JsonMapMetaData>();
        for (int i = 0; i < jsonMapFilesMetaData.mapFiles.Count; i++)
        {
            jsonMapMetaDatas.Add(JsonUtility.FromJson<JsonMapMetaData>(jsonMapFilesMetaData.mapFiles[i]));
        }

        return jsonMapMetaDatas;
    }

    public static void SaveMapMetaData(List<JsonMapMetaData> jsonMapMetaData)
    {
        if (!m_hasInit)
            Init();

        jsonMapMetaData = jsonMapMetaData.OrderBy(j => j.mapID).ToList();

        JsonMapFilesMetaData jsonMapFilesMetaData = new JsonMapFilesMetaData
        {
            mapFiles = new List<string>()
        };

        for (int i = 0; i < jsonMapMetaData.Count; i++)
        {
            jsonMapFilesMetaData.mapFiles.Add(JsonUtility.ToJson(jsonMapMetaData[i]));
        }

        string jsonData = JsonUtility.ToJson(jsonMapFilesMetaData);
        File.WriteAllText(applicationDataPath + mapMetaDataPath, jsonData);
    }
}
