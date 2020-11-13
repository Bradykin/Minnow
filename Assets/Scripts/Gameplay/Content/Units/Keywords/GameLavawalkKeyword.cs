using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLavawalkKeyword : GameKeywordBase
{
    public GameLavawalkKeyword()
    {
        m_name = "Lavawalk";
        m_focusInfoText = "This creature doesn't take damage from lava terrain. Lava tiles only cost 1 Stamina to move through.";
        m_keywordParamType = KeywordParamType.NoParams;
    }

    public override void AddKeyword(GameKeywordBase toAdd)
    {
        //Stacking this keyword does nothing.
    }

    public override void SubtractKeyword(GameKeywordBase toSubtract)
    {
        //Subtracting this keyword does nothing.
    }

    public override bool ShouldBeRemoved()
    {
        return false;
    }

    //Left blank intentionally
    public override string GetDesc()
    {
        return "";
    }

    public override JsonGameKeywordData SaveToJson()
    {
        JsonGameKeywordData jsonData = new JsonGameKeywordData
        {
            name = m_name
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Nothing currently needs to be done here.
    }
}
