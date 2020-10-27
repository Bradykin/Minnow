using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameTileData
{
    public int gridPositionX;
    public int gridPositionY;

    public JsonGameUnitData gameUnitData;
    public JsonGameBuildingData gameBuildingData;
    public JsonGameTerrainData gameTerrainData;
    public JsonGameSpawnPointData gameSpawnPointData;

    public List<int> gameEventMarkers;
}
