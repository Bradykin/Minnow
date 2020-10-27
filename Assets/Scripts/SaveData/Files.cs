using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class Files
{
    //Save related file path data
    public const string REMOTE_DATA_PATH = "RemoteData";
    public const string ADD_TO_BUILD_PATH = "AddToBuild";
    public const string BUILD_DATA_PATH = "BuildData";
    public const string MAP_DATA_PATH = "MapData";
    public const string MAP_META_DATA_PATH = "MapMetaData.txt";
    public const string DEFAULT_MAP_DATA_PATH = "MapData0.txt";
    public const string PLAYER_ACCOUNT_DATA_PATH = "PlayerAccountData.player";

    public static string EDITOR_PATH = Path.Combine(new DirectoryInfo(Application.dataPath).Parent.FullName, REMOTE_DATA_PATH, ADD_TO_BUILD_PATH);
    public static string BUILD_PATH = Path.Combine(Application.productName + "_Data", BUILD_DATA_PATH);

    public static PlayerAccountData ImportPlayerAccountData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(Files.EDITOR_PATH, PLAYER_ACCOUNT_DATA_PATH);
#else
        path = Path.Combine(Files.BUILD_PATH, PLAYER_ACCOUNT_DATA_PATH);
#endif

        if (!File.Exists(path))
        {
            PlayerAccountData data = new PlayerAccountData();
            return data;
        }

        var loaded = JsonConvert.DeserializeObject<PlayerAccountData>(File.ReadAllText(path));
        return loaded;
    }

    public static string ExportPlayerAccountData(PlayerAccountData playerAccountData)
    {
        var export = JsonConvert.SerializeObject(playerAccountData);
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(Files.EDITOR_PATH, PLAYER_ACCOUNT_DATA_PATH), export);
#else
        File.WriteAllText(Path.Combine(Files.BUILD_PATH, PLAYER_ACCOUNT_DATA_PATH), export);
#endif

        return export;
    }

    public static PlayerAccountData ClearPlayerAccountData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(Files.EDITOR_PATH, PLAYER_ACCOUNT_DATA_PATH);
#else
        path = Path.Combine(Files.BUILD_PATH, PLAYER_ACCOUNT_DATA_PATH);
#endif

        File.Delete(path);

        return ImportPlayerAccountData();
    }
}
