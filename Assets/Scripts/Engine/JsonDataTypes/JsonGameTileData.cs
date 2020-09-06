using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JsonGameTileData
{
    public Vector2Int m_gridPosition;
    public JsonGameEntityData m_gameEntityData;
    public JsonGameBuildingData m_gameBuildingData;
    public JsonGameTerrainData m_gameTerrainData;
    public JsonGameEventData m_gameEventData;
}
