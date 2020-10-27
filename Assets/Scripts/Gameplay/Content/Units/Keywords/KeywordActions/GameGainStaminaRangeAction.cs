﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGainStaminaRangeAction : GameAction
{
    private GameUnit m_unit;
    private int m_toGain;
    private int m_range;

    public GameGainStaminaRangeAction(GameUnit unit, int toGain, int range)
    {
        m_unit = unit;
        m_toGain = toGain;
        m_range = range;

        m_name = "Gain Stamina";
        m_actionParamType = ActionParamType.UnitIntParam;
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_range, 0);
        
        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].m_occupyingUnit.m_isDead && tilesInRange[i].m_occupyingUnit.GetTeam() == Team.Player)
            {
                tilesInRange[i].m_occupyingUnit.GainStamina(m_toGain);
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainStaminaRangeAction tempAction = (GameGainStaminaRangeAction)toAdd;

        //Use the greater of the two ranges; don't add them.
        if (tempAction.m_range > m_range)
        {
            m_range = tempAction.m_range;
        }

        m_toGain += tempAction.m_toGain;
    }

    public override string GetDesc()
    {
        return "All friendly units within range " + m_range + " gain " + m_toGain + " Stamina";
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_toGain
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
