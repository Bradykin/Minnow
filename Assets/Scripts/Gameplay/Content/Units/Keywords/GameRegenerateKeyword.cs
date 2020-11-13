using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRegenerateKeyword : GameKeywordBase
{
    public int m_regenVal;

    public GameRegenerateKeyword(int regenVal)
    {
        m_regenVal = regenVal;

        m_name = "Regen";
        m_focusInfoText = "Regenerate health at the start of each turn.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "" + m_regenVal;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameRegenerateKeyword tempKeyword = (GameRegenerateKeyword)toAdd;

        m_regenVal += tempKeyword.m_regenVal;
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameRegenerateKeyword tempKeyword = (GameRegenerateKeyword)toSubtract;

        m_regenVal -= tempKeyword.m_regenVal;
    }

    public override bool ShouldBeRemoved()
    {
        return m_regenVal <= 0;
    }

    public override JsonGameKeywordData SaveToJson()
    {
        JsonGameKeywordData jsonData = new JsonGameKeywordData
        {
            name = m_name,
            intValue = m_regenVal
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
