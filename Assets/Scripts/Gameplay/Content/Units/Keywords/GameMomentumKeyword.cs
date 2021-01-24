using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMomentumKeyword : GameActionKeywordBase
{
    public GameMomentumKeyword(GameAction action)
    {
        m_actions.Add(action);

        m_name = "Momentum";
        m_focusInfoText = "Triggers when this unit attacks another unit or building.";
        m_shortDesc = "On attack";
        m_keywordParamType = KeywordParamType.ActionParam;
    }

    public override void DoAction()
    {
        Debug.LogError("GameMomentumKeyword is using the wrong DoAction - Use DoAction(GameUnit gameUnit) instead");
    }

    public void DoAction(GameUnit targetUnit)
    {
        for (int i = 0; i < m_actions.Count; i++)
        {
            if (m_actions[i] == null)
            {
                continue;
            }

            if (m_actions[i] is GameApplyKeywordToOtherOnMomentumAction applyKeywordToOtherOnMomentumAction)
            {
                if (targetUnit != null)
                {
                    applyKeywordToOtherOnMomentumAction.DoAction(targetUnit);
                }
            }
            else
            {
                m_actions[i].DoAction();
            }
        }
    }
}
