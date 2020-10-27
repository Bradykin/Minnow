using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonTileData
{
    public Vector2Int gridPosition;
    public string gameUnitData;
    public string gameBuildingData;
    public string gameTerrainData;
    public string gameSpawnPointData;

    public List<int> gameEventMarkers;
}
