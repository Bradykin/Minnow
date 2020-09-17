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
        m_actions.Add(new GameSpellcraftAction());

        m_hasInit = true;
    }

    public static GameAction GetActionWithName(JsonActionData jsonData, GameEntity gameEntity)
    {
        if (!m_hasInit)
            Init();

        string actionName = jsonData.name;

        int i = m_actions.FindIndex(t => t.m_name == actionName);

        GameAction newAction;
        switch (m_actions[i].m_actionParamType)
        {
            case GameAction.ActionParamType.NoParams:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType());
                break;
            case GameAction.ActionParamType.IntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), jsonData.intValue1);
                break;
            case GameAction.ActionParamType.TwoIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), jsonData.intValue1, jsonData.intValue2);
                break;
            case GameAction.ActionParamType.EntityParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameEntity);
                break;
            case GameAction.ActionParamType.EntityIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameEntity, jsonData.intValue1);
                break;
            case GameAction.ActionParamType.EntityTwoIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameEntity, jsonData.intValue1, jsonData.intValue2);
                break;
            case GameAction.ActionParamType.GameWalletParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), JsonUtility.FromJson<GameWallet>(jsonData.gameWalletJsonValue));
                break;
            default:
                return null;
        }
        newAction.LoadFromJson(jsonData);

        return newAction;
    }
}
