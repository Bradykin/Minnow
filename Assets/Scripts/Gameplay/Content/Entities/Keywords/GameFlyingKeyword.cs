using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlyingKeyword : GameKeywordBase
{
    public GameFlyingKeyword()
    {
        m_name = "Flying";
        m_desc = "All terrain only takes 1 AP to move over; can fly over impassable terrain.  Don't benefit from terrain protection.";
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
