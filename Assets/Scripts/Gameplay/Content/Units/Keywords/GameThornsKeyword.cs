using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameThornsKeyword : GameKeywordBase
{
    public int m_thornsDamage;

    public GameThornsKeyword(int thornsDamage)
    {
        m_thornsDamage = thornsDamage;

        m_name = "Thorns";
        m_focusInfoText = "When damaged by a unit, deals damage back to the attacker.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "" + m_thornsDamage;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameThornsKeyword tempKeyword = (GameThornsKeyword)toAdd;

        m_thornsDamage += tempKeyword.m_thornsDamage;
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameThornsKeyword tempKeyword = (GameThornsKeyword)toSubtract;

        m_thornsDamage -= tempKeyword.m_thornsDamage;
    }

    public override bool ShouldBeRemoved()
    {
        return m_thornsDamage <= 0;
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_thornsDamage
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
