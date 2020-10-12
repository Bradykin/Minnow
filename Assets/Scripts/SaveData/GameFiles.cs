﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class GameFiles
{
    //Save related file path data
    public const string REMOTE_DATA_PATH = "RemoteData";
    public const string ADD_TO_BUILD_PATH = "AddToBuild";
    public const string BUILD_DATA_PATH = "BuildData";
    public const string MAP_META_DATA_PATH = "MapMetaData.txt";
    public const string DEFAULT_GRID_DATA_PATH = "JsonGridData0.txt";
    public const string PLAYER_SAVE_DATA_PATH = "PlayerSaveData.txt";

    public static string EDITOR_PATH = Path.Combine(new DirectoryInfo(Application.dataPath).Parent.FullName, REMOTE_DATA_PATH, ADD_TO_BUILD_PATH);
    public static string BUILD_PATH = Path.Combine(Application.productName + "_Data", BUILD_DATA_PATH);

    public static GamePlayerSaveData ImportPlayerSaveData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(GameFiles.EDITOR_PATH, PLAYER_SAVE_DATA_PATH);
#else
        path = Path.Combine(GameFiles.BUILD_PATH, PLAYER_SAVE_DATA_PATH);
#endif

        if (!File.Exists(path))
        {
            GamePlayerSaveData data = new GamePlayerSaveData();
            return data;
        }

        var loaded = JsonConvert.DeserializeObject<GamePlayerSaveData>(File.ReadAllText(path));
        return loaded;
    }

    public static string ExportPlayerSaveData(GamePlayerSaveData playerSaveData)
    {
        var export = JsonConvert.SerializeObject(playerSaveData);
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(GameFiles.EDITOR_PATH, PLAYER_SAVE_DATA_PATH), export);
#else
        File.WriteAllText(Path.Combine(GameFiles.BUILD_PATH, PLAYER_SAVE_DATA_PATH), export);
#endif

        return export;
    }

    public static GamePlayerSaveData ClearPlayerSaveData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(GameFiles.EDITOR_PATH, PLAYER_SAVE_DATA_PATH);
#else
        path = Path.Combine(GameFiles.BUILD_PATH, PLAYER_SAVE_DATA_PATH);
#endif

        File.Delete(path);

        return ImportPlayerSaveData();
    }
}
