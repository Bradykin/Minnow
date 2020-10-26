using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct JsonGridData
{
    public int gridSizeX;
    public int gridSizeY;
    public List<string> jsonTileData;
}
