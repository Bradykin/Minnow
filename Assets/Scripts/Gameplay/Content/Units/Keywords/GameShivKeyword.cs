using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameShivKeyword : GameKeywordBase
{
    public GameShivKeyword()
    {
        m_name = "Shiv";
        m_focusInfoText = "A <b>Shiv</b> is a 0 cost spell that deals 4 damage.";
        m_keywordParamType = KeywordParamType.NoParams;

        m_isVisible = false;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        //Stacking this keyword does nothing.
    }

    public override string GetDesc()
    {
        return "";
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Nothing currently needs to be done here.
    }
}
