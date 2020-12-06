using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBleedKeyword : GameKeywordBase
{
    public int m_bleedAmount;

    public GameBleedKeyword(int bleedAmount)
    {
        m_bleedAmount = bleedAmount;

        m_name = "Bleed";
        m_focusInfoText = $"Takes {m_bleedAmount} damage at the end of each turn. Goes away at the end of a wave, or when this unit gets healed to full.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string GetDesc()
    {
        return " " + m_bleedAmount;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        GameBleedKeyword tempKeyword = (GameBleedKeyword)toAdd;

        m_bleedAmount += tempKeyword.m_bleedAmount;
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        GameBleedKeyword tempKeyword = (GameBleedKeyword)toSubtract;

        m_bleedAmount -= tempKeyword.m_bleedAmount;
    }

    public override bool ShouldBeRemoved()
    {
        return m_bleedAmount <= 0;
    }

    public override JsonGameKeywordData SaveToJson()
    {
        JsonGameKeywordData jsonData = new JsonGameKeywordData
        {
            name = m_name,
            isPermanentValue = m_isPermanent,
            intValue = m_bleedAmount
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
