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
        JsonGridData jsonData;
        if (Globals.mapToLoad == string.Empty)
        {
            Globals.mapToLoad = Files.DEFAULT_GRID_DATA_PATH;
        }

        if (Globals.loadingRun)
        {
            jsonData = PlayerDataManager.PlayerAccountData.PlayerRunData.m_jsonGridData;
        }
        else
        {
#if UNITY_EDITOR
            jsonData = JsonConvert.DeserializeObject<JsonGridData>(File.ReadAllText(Path.Combine(Files.EDITOR_PATH, Globals.mapToLoad)));
#else
            jsonData = JsonConvert.DeserializeObject<JsonGridData>(File.ReadAllText(Path.Combine(Files.BUILD_PATH, Globals.mapToLoad)));
#endif
        }

        Globals.mapToLoad = string.Empty;
        WorldGridManager.Instance.LoadFromJson(jsonData);
        WorldGridManager.Instance.Setup(transform);

        WorldController.Instance.m_gameController.LateInit();
    }

    public virtual void Reset()
    {
        WorldGridManager.Instance.RecycleGrid();        
    }
}
