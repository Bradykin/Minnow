using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDamageReductionKeyword : GameKeywordBase
{
    public int m_damageReduction;
    public bool m_buffedByTerrain;

    public GameDamageReductionKeyword(int damageReduction, bool buffedByTerrain = false)
    {
        m_damageReduction = damageReduction;
        m_buffedByTerrain = buffedByTerrain;

        m_name = "Damage Reduction";
        m_focusInfoText = "Takes less damage from all sources.";
        m_keywordParamType = KeywordParamType.IntBoolParam;
    }

    public override string GetDesc()
    {
        if (m_buffedByTerrain)
        {
            return m_damageReduction + " (terrain buffed)";
        }
        else
        {
            return "" + m_damageReduction;
        }
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameDamageReductionKeyword tempKeyword = (GameDamageReductionKeyword)toAdd;

        m_damageReduction += tempKeyword.m_damageReduction;

        if (tempKeyword.m_buffedByTerrain)
        {
            m_buffedByTerrain = true;
        }
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

    public override JsonGameKeywordData SaveToJson()
    {
        JsonGameKeywordData jsonData = new JsonGameKeywordData
        {
            name = m_name,
            intValue = m_damageReduction,
            boolValue = m_buffedByTerrain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
