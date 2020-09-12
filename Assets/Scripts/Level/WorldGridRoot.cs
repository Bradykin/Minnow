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
            jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(Globals.GetDefaultGridDataPath()));
        }
        else
        {
            jsonData = JsonUtility.FromJson<JsonGridData>(File.ReadAllText(Globals.mapToLoad));
            Globals.mapToLoad = string.Empty;
        }
        WorldGridManager.Instance.LoadFromJson(jsonData);
        WorldGridManager.Instance.Setup(transform);
    }

    public virtual void Reset()
    {
        WorldGridManager.Instance.RecycleGrid();        
    }
}
