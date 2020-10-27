using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlyingKeyword : GameKeywordBase
{
    public GameFlyingKeyword()
    {
        m_name = "Flying";
        m_focusInfoText = "All terrain only takes 1 Stamina to move over; can fly over impassable terrain.  Don't benefit from terrain protection.";
        m_keywordParamType = KeywordParamType.NoParams;
    }

    public override string GetDesc()
    {
        return "";
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        //Stacking this keyword does nothing.
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
