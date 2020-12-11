using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBleedKeyword : GameKeywordBase
{
    public GameBleedKeyword()
    {
        m_name = "Bleed";
        m_focusInfoText = $"Regenerates 2 less stamina each turn. Bleeding is removed if this unit is healed to full.";
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
            name = m_name,
            isPermanentValue = m_isPermanent
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
