using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFadeKeyword : GameKeywordBase
{
    public bool m_isActive;
    
    public GameFadeKeyword(bool isActive = true)
    {
        m_isActive = isActive;

        m_name = "Fade";
        m_focusInfoText = "Cannot be targeted with attacks, spells, or abilities by the opposing team. Fade is lost when this unit attacks.";
        m_keywordParamType = KeywordParamType.BoolParam;
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
            name = m_name,
            boolValue = m_isActive,
            isPermanentValue = m_isPermanent
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Nothing currently needs to be done here.
    }
}
