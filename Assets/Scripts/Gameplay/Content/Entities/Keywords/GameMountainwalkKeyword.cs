using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMountainwalkKeyword : GameKeywordBase
{
    public GameMountainwalkKeyword()
    {
        m_name = "Mountainwalk";
        m_desc = "Can move on mountain tiles.  Hills only cost 1 AP to move across.";
        m_focusInfoText = m_desc;
        m_keywordParamType = KeywordParamType.NoParams;
    }

    public override string SaveToJson()
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