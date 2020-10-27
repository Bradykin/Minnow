using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrittleKeyword : GameKeywordBase
{
    public int m_amount;

    public GameBrittleKeyword(int amount)
    {
        m_amount = amount;

        m_name = "Brittle";
        m_focusInfoText = "Takes additional damage.";
        m_shortDesc = "Takes more damage on hit";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameBrittleKeyword tempKeyword = (GameBrittleKeyword)toAdd;

        m_amount += tempKeyword.m_amount;
    }

    public override string GetDesc()
    {
        return "" + m_amount;
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_amount
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
