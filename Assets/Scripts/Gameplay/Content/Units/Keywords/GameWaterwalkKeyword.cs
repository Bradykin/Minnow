using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaterwalkKeyword : GameKeywordBase
{
    public GameWaterwalkKeyword()
    {
        m_name = "Waterwalk";
        m_focusInfoText = "Can move on water tiles.";
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
