using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonGameCardData
{
    public string baseName;

    public JsonGameUnitData jsonGameUnitData;
    public int? jsonGameUnitXPosition;
    public int? jsonGameUnitYPosition;
}