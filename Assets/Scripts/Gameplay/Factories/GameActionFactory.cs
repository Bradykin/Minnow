﻿using System;
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
        m_actions.Add(new GameGainAPAction(null, 0));
        m_actions.Add(new GameGainEnergyAction(0));
        m_actions.Add(new GameGainPowerAction(null, 0));
        m_actions.Add(new GameGainResourceAction(null));

        m_hasInit = true;
    }

    public static GameAction GetActionWithName(string actionName)
    {
        if (!m_hasInit)
            Init();

        int i = m_actions.FindIndex(t => t.m_name == actionName);

        GameAction newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType());

        return newAction;
    }
}
