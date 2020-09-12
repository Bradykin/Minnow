using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapDifficulty : int
{
    Easy,
    Medium,
    Hard
}

public struct JsonMapMetaData
{
    public string mapName;
    public int mapID;
    public Vector2Int gridSize;
    public int mapDifficulty;
    public string dataPath;
}
