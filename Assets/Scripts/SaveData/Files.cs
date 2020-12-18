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
    public const string PLAYER_RUN_DATA_PATH = "PlayerRunData.player";
    public const string GAME_DIRECTOR_STATISTICS_DATA_PATH = "GameDirectorStatistics.player";

    public static string EDITOR_PATH = Path.Combine(new DirectoryInfo(Application.dataPath).Parent.FullName, REMOTE_DATA_PATH, ADD_TO_BUILD_PATH);
    public static string BUILD_PATH = Path.Combine(Application.productName + "_Data", BUILD_DATA_PATH);
    public static string PERSISTENT_SAVE_BUILD_PATH = Application.persistentDataPath;

    public static PlayerAccountData ImportPlayerAccountData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(Files.EDITOR_PATH, PLAYER_ACCOUNT_DATA_PATH);
#else
        path = Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, PLAYER_ACCOUNT_DATA_PATH);
#endif

        if (!File.Exists(path))
        {
            PlayerAccountData data = new PlayerAccountData();
            return data;
        }

        var loaded = JsonConvert.DeserializeObject<PlayerAccountData>(File.ReadAllText(path));

        if (loaded.JsonGameMetaProgressionRewardDatas != null)
        {
            for (int i = 0; i < loaded.JsonGameMetaProgressionRewardDatas.Count; i++)
            {
                GameMetaprogressionReward loadedReward = new GameMetaprogressionReward();
                loadedReward.LoadFromJson(loaded.JsonGameMetaProgressionRewardDatas[i]);

                UIMetaprogressionNotificationController.AddReward(loadedReward);
            }
        }

        return loaded;
    }

    public static string ExportPlayerAccountData(PlayerAccountData playerAccountData)
    {
        playerAccountData.SaveGameMetaProgressionRewardDatas();

        var export = JsonConvert.SerializeObject(playerAccountData);
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(Files.EDITOR_PATH, PLAYER_ACCOUNT_DATA_PATH), export);
#else
        File.WriteAllText(Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, PLAYER_ACCOUNT_DATA_PATH), export);
#endif

        return export;
    }

    public static PlayerAccountData ClearPlayerAccountData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(Files.EDITOR_PATH, PLAYER_ACCOUNT_DATA_PATH);
#else
        path = Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, PLAYER_ACCOUNT_DATA_PATH);
#endif

        File.Delete(path);

        return ImportPlayerAccountData();
    }

    public static PlayerRunData ImportPlayerRunData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(Files.EDITOR_PATH, PLAYER_RUN_DATA_PATH);
#else
        path = Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, PLAYER_RUN_DATA_PATH);
#endif

        if (!File.Exists(path))
        {
            PlayerRunData data = new PlayerRunData();
            return data;
        }

        var loaded = JsonConvert.DeserializeObject<PlayerRunData>(File.ReadAllText(path));

        return loaded;
    }

    public static string ExportPlayerRunData(PlayerRunData playerRunData)
    {
        var export = JsonConvert.SerializeObject(playerRunData);
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(Files.EDITOR_PATH, PLAYER_RUN_DATA_PATH), export);
#else
        File.WriteAllText(Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, PLAYER_RUN_DATA_PATH), export);
#endif

        return export;
    }

    public static PlayerRunData ClearPlayerRunData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(Files.EDITOR_PATH, PLAYER_RUN_DATA_PATH);
#else
        path = Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, PLAYER_RUN_DATA_PATH);
#endif

        File.Delete(path);

        return ImportPlayerRunData();
    }

    public static GameDirectorStatistics ImportGameDirectorStatisticsData()
    {
        string path;
#if UNITY_EDITOR
        path = Path.Combine(Files.EDITOR_PATH, GAME_DIRECTOR_STATISTICS_DATA_PATH);
#else
        path = Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, GAME_DIRECTOR_STATISTICS_DATA_PATH);
#endif

        if (!File.Exists(path))
        {
            GameDirectorStatistics data = new GameDirectorStatistics();
            return data;
        }

        var loaded = JsonConvert.DeserializeObject<GameDirectorStatistics>(File.ReadAllText(path));

        return loaded;
    }

    public static string ExportGameDirectorData(GameDirectorStatistics gameDirectorStatistics)
    {
        var export = JsonConvert.SerializeObject(gameDirectorStatistics);
#if UNITY_EDITOR
        File.WriteAllText(Path.Combine(Files.EDITOR_PATH, GAME_DIRECTOR_STATISTICS_DATA_PATH), export);
#else
        File.WriteAllText(Path.Combine(Files.PERSISTENT_SAVE_BUILD_PATH, GAME_DIRECTOR_STATISTICS_DATA_PATH), export);
#endif

        return export;
    }
}
