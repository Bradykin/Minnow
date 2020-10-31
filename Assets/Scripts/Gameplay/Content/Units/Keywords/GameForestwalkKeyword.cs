using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameForestwalkKeyword : GameKeywordBase
{
    public GameForestwalkKeyword()
    {
        m_name = "Forestwalk";
        m_focusInfoText = "Forests only cost 1 Stamina to move through.";
        m_keywordParamType = KeywordParamType.NoParams;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        //Stacking this keyword does nothing.
    }

    //Left blank intentionally
    public override string GetDesc()
    {
        return "";
    }

    public override JsonKeywordData SaveToJson()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Nothing currently needs to be done here.
    }
}