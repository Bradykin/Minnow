using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRangeKeyword : GameKeywordBase
{
    public int m_range;
    public bool m_buffedByTerrain;

    public GameRangeKeyword(int range, bool buffedByTerrain = false)
    {
        m_range = range;
        m_buffedByTerrain = buffedByTerrain;

        m_name = "Ranged";
        m_focusInfoText = "Can attack at range.";
        m_keywordParamType = KeywordParamType.IntBoolParam;
    }

    public override string GetDesc()
    {
        if (m_buffedByTerrain)
        {
            return m_range + " (terrain buffed)";
        }
        else
        {
            return "" + m_range;
        }
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameRangeKeyword tempKeyword = (GameRangeKeyword)toAdd;

        m_range += tempKeyword.m_range;

        if (tempKeyword.m_buffedByTerrain)
        {
            m_buffedByTerrain = true;
        }
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameRangeKeyword tempKeyword = (GameRangeKeyword)toSubtract;

        m_range -= tempKeyword.m_range;
    }

    public override bool ShouldBeRemoved()
    {
        return m_range <= 0;
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_range,
            boolValue = m_buffedByTerrain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
