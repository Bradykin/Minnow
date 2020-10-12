﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRangeKeyword : GameKeywordBase
{
    public int m_range;

    public GameRangeKeyword(int range)
    {
        m_range = range;

        m_name = "Ranged";
        m_desc = "" + m_range;
        m_focusInfoText = "Can attack at range.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public void IncreaseRange(int increase)
    {
        m_range += increase;

        m_name = "Ranged";
        m_desc = "" + m_range;
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_range
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
