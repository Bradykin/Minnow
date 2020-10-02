﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBrittleKeyword : GameKeywordBase
{
    public int m_amount;

    public GameBrittleKeyword(int amount)
    {
        m_amount = amount;

        m_name = "Brittle";
        if (m_amount > 0)
        {
            m_desc = "" + m_amount;
        }
        m_focusInfoText = "Takes additional damage.";
        m_shortDesc = "Takes more damage on hit";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public void IncreaseAmount(int increase)
    {
        m_amount += increase;

        m_name = "Brittle";
        m_desc = "" + m_amount;
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_amount
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
