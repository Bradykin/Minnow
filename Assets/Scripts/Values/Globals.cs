using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Globals
{
    //TODO: THis is temp. This datapath should be gotten from the menu before loading the level
    private static string mapMetaDataPath;
    private static string defaultGridDataPath;

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
        mapMetaDataPath = Application.dataPath + "/RemoteData/SaveMetaData.txt";
        defaultGridDataPath = Application.dataPath + "/RemoteData/JsonGridData0.txt";

        m_hasInit = true;
    }

    public static string GetMapMetaDataPath()
    {
        if (!m_hasInit)
            Init();

        return mapMetaDataPath;
    }

    public static string GetDefaultGridDataPath()
    {
        if (!m_hasInit)
            Init();

        return defaultGridDataPath;
    }

    public static List<JsonMapMetaData> LoadMapMetaData()
    {
        if (!m_hasInit)
            Init();
        
        if (!File.Exists(mapMetaDataPath))
        {
            return new List<JsonMapMetaData>();
        }
        JsonMapFilesMetaData jsonMapFilesMetaData = JsonUtility.FromJson<JsonMapFilesMetaData>(File.ReadAllText(mapMetaDataPath));

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

        JsonMapFilesMetaData jsonMapFilesMetaData = new JsonMapFilesMetaData
        {
            mapFiles = new List<string>()
        };

        for (int i = 0; i < jsonMapMetaData.Count; i++)
        {
            jsonMapFilesMetaData.mapFiles.Add(JsonUtility.ToJson(jsonMapMetaData[i]));
        }

        string jsonData = JsonUtility.ToJson(jsonMapFilesMetaData);
        File.WriteAllText(mapMetaDataPath, jsonData);
    }
}
