using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCleaveKeyword : GameKeywordBase
{
    public GameCleaveKeyword()
    {
        m_name = "Cleave";
        m_focusInfoText = "When this unit attacks, it also hits all enemy targets that are adjacent both to itself and the target. Does not work with ranged.";
        m_keywordParamType = KeywordParamType.NoParams;
    }

    //Left blank intentionally
    public override string GetDesc()
    {
        return "";
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
