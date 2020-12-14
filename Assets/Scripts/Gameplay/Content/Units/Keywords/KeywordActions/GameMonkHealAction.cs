using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMonkHealAction : GameAction
{
    private GameUnit m_unit;
    private List<int> m_ranges;

    public GameMonkHealAction(GameUnit unit, int range)
    {
        m_unit = unit;
        m_ranges = new List<int>();
        m_ranges.Add(range);

        m_name = "Monk Heal Range";
        m_actionParamType = ActionParamType.UnitListIntParam;
    }

    public GameMonkHealAction(GameUnit unit, List<int> ranges)
    {
        m_unit = unit;
        m_ranges = new List<int>();
        m_ranges.AddRange(ranges);

        m_name = "Monk Heal Range";
        m_actionParamType = ActionParamType.UnitListIntParam;
    }

    public override string GetDesc()
    {
        return $"Heal all allied units (including self) in range {m_ranges.Max()} for an amount equal to {m_unit.GetName()}'s power, then remove bonus power.";
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_ranges.Max(), 0);

        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].GetOccupyingUnit().m_isDead && tilesInRange[i].GetOccupyingUnit().GetTeam() == m_unit.GetTeam())
            {
                tilesInRange[i].GetOccupyingUnit().Heal(m_unit.GetPower());
            }
        }

        ((ContentArmouredMonk)m_unit).ResetPower();
    }

    public override void AddAction(GameAction toAdd)
    {
        GameMonkHealAction tempAction = (GameMonkHealAction)toAdd;

        m_ranges.AddRange(tempAction.m_ranges);
    }

    public override void SubtractAction(GameAction toAdd)
    {
        GameMonkHealAction tempAction = (GameMonkHealAction)toAdd;

        for (int i = 0; i < tempAction.m_ranges.Count; i++)
        {
            m_ranges.Remove(tempAction.m_ranges[i]);
        }
    }

    public override bool ShouldBeRemoved()
    {
        return m_ranges.Count == 0;
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
            intListValue1 = m_ranges
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
