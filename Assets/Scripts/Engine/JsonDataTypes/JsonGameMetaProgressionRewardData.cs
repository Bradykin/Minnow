using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct JsonGameMetaProgressionRewardData
{
    public string title;
    public string desc;

    public List<JsonGameCardData> jsonGameCardDatas;
    public List<JsonGameRelicData> jsonGameRelicDatas;
    public int mapId;
    public JsonGameCardData jsonStarterGameCardData;
    public JsonGameRelicData jsonStarterGameRelicData;
}