using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnrageKeyword : GameActionKeywordBase
{
    public GameEnrageKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Enrage";
        m_focusInfoText = "Triggers when this unit takes damage.";
        m_shortDesc = "On take damage";
        m_keywordParamType = KeywordParamType.ActionParam;
    }

    public void DoAction(int damageAmount)
    {
        for (int i = 0; i < m_actions.Count; i++)
        {
            if (m_actions[i] is GameGainGoldEnrageAction gainGoldEnrageAction)
            {
                gainGoldEnrageAction.DoAction(damageAmount);
            }
            else
            {
                m_actions[i].DoAction();
            }
        }
    }
}
