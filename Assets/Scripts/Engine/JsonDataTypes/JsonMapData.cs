using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct JsonMapData
{
    public int gridSizeX;
    public int gridSizeY;
    public List<JsonGameTileData> jsonGameTileData;
}
