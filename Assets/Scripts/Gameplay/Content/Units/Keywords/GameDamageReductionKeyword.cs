using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDamageReductionKeyword : GameKeywordBase
{
    public int m_damageReduction;

    public GameDamageReductionKeyword(int damageReduction)
    {
        m_damageReduction = damageReduction;

        m_name = "Damage Reduction";
        m_focusInfoText = "Takes less damage from all sources.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string GetDesc()
    {
        return "" + m_damageReduction;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameDamageReductionKeyword tempKeyword = (GameDamageReductionKeyword)toAdd;

        m_damageReduction += tempKeyword.m_damageReduction;
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameDamageReductionKeyword tempKeyword = (GameDamageReductionKeyword)toSubtract;

        m_damageReduction -= tempKeyword.m_damageReduction;
    }

    public override bool ShouldBeRemoved()
    {
        return m_damageReduction <= 0;
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_damageReduction
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
