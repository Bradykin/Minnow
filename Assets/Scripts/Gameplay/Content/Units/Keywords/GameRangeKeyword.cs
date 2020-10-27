using Newtonsoft.Json;
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
        m_focusInfoText = "Can attack at range.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameRangeKeyword tempKeyword = (GameRangeKeyword)toAdd;

        m_range += tempKeyword.m_range;
    }

    public override string GetDesc()
    {
        return "" + m_range;
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_range
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
