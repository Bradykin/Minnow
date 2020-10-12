using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpellcraftKeyword : GameKeywordBase
{
    private GameAction m_action;
    public readonly static int m_spellcraftRange = 3;

    public GameSpellcraftKeyword(GameAction action)
    {
        m_action = action;

        m_name = "Spellcraft";
        m_focusInfoText = "Triggers when the player casts a spell card that has no target, or is targeted within " + m_spellcraftRange + " tiles of this unit.";
        m_shortDesc = "On spell cast within 3 range.";
        m_keywordParamType = KeywordParamType.ActionParam;

        if (action == null)
        {
            return;
        }

        m_desc = action.m_desc;
    }

    public void DoAction()
    {
        m_action.DoAction();
    }

    public override string SaveToJsonAsString()
    {
        JsonKeywordData jsonData = new JsonKeywordData
        {
            name = m_name,
            actionJson = m_action.SaveToJson()
        };

        var export = JsonConvert.SerializeObject(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
