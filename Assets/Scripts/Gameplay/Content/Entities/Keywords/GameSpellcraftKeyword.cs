using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpellcraftKeyword : GameActionKeywordBase
{
    public readonly static int m_spellcraftRange = 3;

    public GameSpellcraftKeyword(GameAction action)
    {
        m_actions.Add(action);

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
}
