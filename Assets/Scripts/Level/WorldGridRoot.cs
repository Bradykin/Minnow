using Game.Util;
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
            Globals.mapToLoad = Constants.DEFAULT_GRID_DATA_PATH;
/*#if UNITY_EDITOR
            jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(Path.Combine(Constants.EDITOR_PATH, Constants.DEFAULT_GRID_DATA_PATH)));
#else
            jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(Path.Combine(Constants.BUILD_PATH, Constants.DEFAULT_GRID_DATA_PATH)));
#endif*/
        }

#if UNITY_EDITOR
        jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(Path.Combine(Constants.EDITOR_PATH, Globals.mapToLoad)));
#else
        jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(Path.Combine(Constants.BUILD_PATH, Globals.mapToLoad)));
#endif
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
