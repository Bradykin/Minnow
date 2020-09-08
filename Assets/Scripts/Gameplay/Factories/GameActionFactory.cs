using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameActionFactory
{
    private static List<GameAction> m_actions = new List<GameAction>();

    private static bool m_hasInit = false;

    public static void Init()
    {
        m_actions.Add(new GameDrawCardAction(0));
        m_actions.Add(new GameDeathAction(null));
        m_actions.Add(new GameExplodeAction(null, 0, 0));
        m_actions.Add(new GameFullHealAction(null));
        m_actions.Add(new GameHealAction(null, 0));
        m_actions.Add(new GameGainAPAction(null, 0));
        m_actions.Add(new GameGainEnergyAction(0));
        m_actions.Add(new GameGainPowerAction(null, 0));
        m_actions.Add(new GameGainRangeAction(null, 0));
        m_actions.Add(new GameGainResourceAction(null));
        m_actions.Add(new GainPurpleBeamAction(0));

        m_hasInit = true;
    }

    public static GameAction GetActionWithName(JsonKeywordData jsonData)
    {
        if (!m_hasInit)
            Init();

        string actionName = jsonData.actionName;

        int i = m_actions.FindIndex(t => t.m_name == actionName);

        GameAction newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType());

        return newAction;
    }
}
