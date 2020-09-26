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
        if (Constants.UseLocationalSpellcraft)
        {
            m_focusInfoText = "Triggers when the player cast a spell card that has no target, or is targeted within " + m_spellcraftRange + " tiles of this entity.";
        }
        else
        {
            m_focusInfoText = "Triggers when the player casts a spell card.";
        }
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

        var export = JsonUtility.ToJson(jsonData);

        return export;
    }

    public override void LoadFromJson(JsonKeywordData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
