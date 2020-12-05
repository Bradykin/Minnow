using Newtonsoft.Json;
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
        m_actions.Add(new GameApplyKeywordToOtherOnMomentumAction(null, null));
        m_actions.Add(new GameDrawCardAction(0));
        m_actions.Add(new GameGainShivAction(0));
        m_actions.Add(new GameDeathAction(null));
        m_actions.Add(new GameDoublePowerAction(null, 0));
        m_actions.Add(new GameExplodeAction(null, 0, 0));
        m_actions.Add(new GameFullHealAction(null));
        m_actions.Add(new GameFullHealRangeAction(null, 0));
        m_actions.Add(new GameHealAction(null, 0));
        m_actions.Add(new GameGainDamageShieldAction(null, 0));
        m_actions.Add(new GameGainStatsAction(null, 0, 0));
        m_actions.Add(new GameGainStatsPermanentAction(null, 0, 0));
        m_actions.Add(new GameGainStatsRangeAction(null, 0, 0, 0));
        m_actions.Add(new GameLoseStaminaAction(null, 0));
        m_actions.Add(new GameLoseStatsAction(null, 0, 0));
        m_actions.Add(new GameLoseTempMagicPowerAction(0));
        m_actions.Add(new GameGainStaminaAction(null, 0));
        m_actions.Add(new GameGainStaminaRangeAction(null, 0, 0));
        m_actions.Add(new GameGainStaminaRegenAction(null, 0));
        m_actions.Add(new GameGainTempMagicPowerAction(0));
        m_actions.Add(new GameGainBrittleAction(null, 0));
        m_actions.Add(new GameGainEnergyAction(0));
        m_actions.Add(new GameGainRangeAction(null, 0));
        m_actions.Add(new GameGainGoldAction(0));
        m_actions.Add(new GameGetHitAction(null, 0));
        m_actions.Add(new GameRoarOfVictoryAction(null, 0));
        m_actions.Add(new GameShivNearbyAction(null, 0, 0));
        m_actions.Add(new GameSpellcraftAttackAction(null, 0));
        m_actions.Add(new GameGainKeywordAction(null, null));
        m_actions.Add(new GameLoseKeywordAction(null, null));
        m_actions.Add(new GameGainKeywordUntilEndOfTurnAction(null, null));
        m_actions.Add(new GameGainKeywordRangeAction(null, 0, null));

        m_hasInit = true;
    }

    public static GameAction GetActionFromJson(JsonGameActionData jsonData, GameUnit gameUnit)
    {
        if (!m_hasInit)
            Init();

        string actionName = jsonData.name;

        int i = m_actions.FindIndex(t => t.GetName() == actionName);

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
            case GameAction.ActionParamType.UnitParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit);
                break;
            case GameAction.ActionParamType.UnitIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit, jsonData.intValue1);
                break;
            case GameAction.ActionParamType.UnitTwoIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit, jsonData.intValue1, jsonData.intValue2);
                break;
            case GameAction.ActionParamType.UnitListIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit, jsonData.intListValue1);
                break;
            case GameAction.ActionParamType.UnitIntListIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit, jsonData.intValue1, jsonData.intListValue1);
                break;
            case GameAction.ActionParamType.UnitTwoIntListIntParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit, jsonData.intValue1, jsonData.intValue2, jsonData.intListValue1);
                break;
            case GameAction.ActionParamType.UnitKeywordParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit, GameKeywordFactory.GetKeywordsFromJson(jsonData.gameKeywordData, gameUnit));
                break;
            case GameAction.ActionParamType.UnitListIntKeywordParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), gameUnit, jsonData.intListValue1, GameKeywordFactory.GetKeywordsFromJson(jsonData.gameKeywordData, gameUnit));
                break;
            case GameAction.ActionParamType.GameWalletParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), JsonConvert.DeserializeObject<GameWallet>(jsonData.gameWalletJsonValue));
                break;
            default:
                return null;
        }
        newAction.LoadFromJson(jsonData);

        return newAction;
    }

    public static GameAction GetActionFromJson(JsonGameActionData jsonData)
    {
        if (!m_hasInit)
            Init();

        string actionName = jsonData.name;

        int i = m_actions.FindIndex(t => t.GetName() == actionName);

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
            case GameAction.ActionParamType.GameWalletParam:
                newAction = (GameAction)Activator.CreateInstance(m_actions[i].GetType(), JsonConvert.DeserializeObject<GameWallet>(jsonData.gameWalletJsonValue));
                break;
            default:
                return null;
        }
        newAction.LoadFromJson(jsonData);

        return newAction;
    }
}
