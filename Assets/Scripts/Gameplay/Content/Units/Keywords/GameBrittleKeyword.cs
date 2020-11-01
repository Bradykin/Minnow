using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrittleKeyword : GameKeywordBase
{
    public int m_damageIncrease;

    public GameBrittleKeyword(int damageIncrease)
    {
        m_damageIncrease = damageIncrease;

        m_name = "Brittle";
        m_focusInfoText = "Takes additional damage.";
        m_shortDesc = "Takes more damage on hit";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "" + m_damageIncrease;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameBrittleKeyword tempKeyword = (GameBrittleKeyword)toAdd;

        m_damageIncrease += tempKeyword.m_damageIncrease;
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameBrittleKeyword tempKeyword = (GameBrittleKeyword)toSubtract;

        m_damageIncrease -= tempKeyword.m_damageIncrease;
    }

    public override bool ShouldBeRemoved()
    {
        return m_damageIncrease <= 0;
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_damageIncrease
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
