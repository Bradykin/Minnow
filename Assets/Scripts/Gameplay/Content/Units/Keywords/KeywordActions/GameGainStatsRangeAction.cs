﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGainStatsRangeAction : GameAction
{
    private GameUnit m_unit;
    private int m_attackToGain;
    private int m_healthToGain;
    private List<int> m_ranges;

    public GameGainStatsRangeAction(GameUnit unit, int attackToGain, int healthToGain, int range)
    {
        m_unit = unit;
        m_attackToGain = attackToGain;
        m_healthToGain = healthToGain;
        m_ranges = new List<int>();
        m_ranges.Add(range);

        m_name = "Gain Stats Range";
        m_actionParamType = ActionParamType.UnitTwoIntListIntParam;
    }

    public GameGainStatsRangeAction(GameUnit unit, int attackToGain, int healthToGain, List<int> ranges)
    {
        m_unit = unit;
        m_attackToGain = attackToGain;
        m_healthToGain = healthToGain;
        m_ranges = new List<int>();
        m_ranges.AddRange(ranges);

        m_name = "Gain Stats Range";
        m_actionParamType = ActionParamType.UnitTwoIntListIntParam;
    }

    public override string GetDesc()
    {
        return $"All allied units within range {m_ranges.Max()} gain +{m_attackToGain}/+{m_healthToGain}";
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_ranges.Max(), 0);
        
        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].GetOccupyingUnit().m_isDead && tilesInRange[i].GetOccupyingUnit().GetTeam() == m_unit.GetTeam())
            {
                tilesInRange[i].GetOccupyingUnit().AddStats(m_attackToGain, m_healthToGain, false, false);
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainStatsRangeAction tempAction = (GameGainStatsRangeAction)toAdd;

        m_attackToGain += tempAction.m_attackToGain;
        m_healthToGain += tempAction.m_healthToGain;
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainStatsRangeAction tempAction = (GameGainStatsRangeAction)toSubtract;

        m_attackToGain -= tempAction.m_attackToGain;
        m_healthToGain -= tempAction.m_healthToGain;
    }

    public override bool ShouldBeRemoved()
    {
        return (m_attackToGain <= 0 && m_healthToGain <= 0) || m_ranges.Count == 0;
    }

    public override GameUnit GetGameUnit()
    {
        return m_unit;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            intValue1 = m_attackToGain,
            intValue2 = m_healthToGain,
            intListValue1 = m_ranges
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
