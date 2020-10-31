using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTauntKeyword : GameKeywordBase
{
    public GameTauntKeyword()
    {
        m_name = "Taunt";
        m_focusInfoText = "Enemies will choose to attack this unit over any other possible targets.";
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
