using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWaterwalkKeyword : GameKeywordBase
{
    public GameWaterwalkKeyword()
    {
        m_name = "Waterwalk";
        m_focusInfoText = "Can move on water tiles, and moving on water tiles costs 0 ap.";
        m_keywordParamType = KeywordParamType.NoParams;
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name
        };

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Nothing currently needs to be done here.
    }
}
