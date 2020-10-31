using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMountainwalkKeyword : GameKeywordBase
{
    public GameMountainwalkKeyword()
    {
        m_name = "Mountainwalk";
        m_focusInfoText = "Can move on mountain tiles.  Hills only cost 1 Stamina to move across.";
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