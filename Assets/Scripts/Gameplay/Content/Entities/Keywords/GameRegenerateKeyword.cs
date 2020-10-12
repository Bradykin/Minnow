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
        if (m_regenVal > 0)
        {
            m_desc = "" + m_regenVal;
        }
        m_focusInfoText = "Regenerate health at the end of each turn.";
        m_keywordParamType = KeywordParamType.IntParam;
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            intValue = m_regenVal
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
