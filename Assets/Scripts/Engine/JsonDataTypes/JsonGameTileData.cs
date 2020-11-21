using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonGameTileData
{
    public int gridPositionX;
    public int gridPositionY;

    public JsonGameUnitData gameUnitData;
    public JsonGameBuildingData gameBuildingData;
    public JsonGameTerrainData gameTerrainData;
    public JsonGameSpawnPointData gameSpawnPointData;
    public JsonGameWorldPerkData gameWorldPerkData;

    public List<int> gameEventMarkers;

    public bool isFog = true;
    public bool isSoftFog = false;
    public bool isFogBorder = false;
}
