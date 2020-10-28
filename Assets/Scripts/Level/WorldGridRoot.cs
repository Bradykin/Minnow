using Game.Util;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WorldGridRoot : MonoBehaviour, IReset
{
    public virtual void Activate()
    {
        JsonMapData jsonMapData;
        if (Globals.mapToLoad == string.Empty)
        {
            Globals.mapToLoad = Files.DEFAULT_MAP_DATA_PATH;
        }

        if (Globals.loadingRun)
        {
            jsonMapData = PlayerDataManager.PlayerAccountData.PlayerRunData.m_jsonMapData;
            Globals.mapToLoad = string.Empty;

            WorldGridManager.Instance.LoadFromJson(jsonMapData);
        }
        else
        {
#if UNITY_EDITOR
            jsonMapData = JsonConvert.DeserializeObject<JsonMapData>(File.ReadAllText(Path.Combine(Files.EDITOR_PATH, Globals.mapToLoad)));
#else
            jsonMapData = JsonConvert.DeserializeObject<JsonMapData>(File.ReadAllText(Path.Combine(Files.BUILD_PATH, Globals.mapToLoad)));
#endif

            Globals.mapToLoad = string.Empty;
            WorldGridManager.Instance.LoadFromJson(jsonMapData);
        }
        WorldGridManager.Instance.Setup(transform);

        WorldController.Instance.m_gameController.LateInit();

        if (Globals.loadingRun)
        {
            GameHelper.GetGameController().LoadFromJson(PlayerDataManager.PlayerAccountData.PlayerRunData.m_jsonGameControllerData);
        }
    }

    public virtual void Reset()
    {
        WorldGridManager.Instance.RecycleGrid();        
    }
}
