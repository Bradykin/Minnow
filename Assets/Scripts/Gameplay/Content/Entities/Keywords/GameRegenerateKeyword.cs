using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRegenerateKeyword : GameKeywordBase
{
    public int m_regenVal;

    public GameRegenerateKeyword(int regenVal)
    {
        m_regenVal = regenVal;

        m_name = "Regen " + m_regenVal;
        m_desc = "Restores " + m_regenVal + " health at the start of each turn.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_regenVal
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
