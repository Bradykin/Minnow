using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameFullHealRangeAction : GameAction
{
    private GameUnit m_unit;
    private List<int> m_ranges;

    public GameFullHealRangeAction(GameUnit unit, int range)
    {
        m_unit = unit;
        m_ranges = new List<int>();
        m_ranges.Add(range);

        m_name = "Full Heal Range";
        m_actionParamType = ActionParamType.UnitListIntParam;
    }

    public GameFullHealRangeAction(GameUnit unit, List<int> ranges)
    {
        m_unit = unit;
        m_ranges = new List<int>();
        m_ranges.AddRange(ranges);

        m_name = "Full Heal Range";
        m_actionParamType = ActionParamType.UnitListIntParam;
    }

    public override string GetDesc()
    {
        return $"Fully heal all allied units in range {m_ranges.Max()}";
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_ranges.Max(), 0);
        
        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].m_occupyingUnit.m_isDead && tilesInRange[i].m_occupyingUnit.GetTeam() == m_unit.GetTeam())
            {
                tilesInRange[i].m_occupyingUnit.Heal(tilesInRange[i].m_occupyingUnit.GetMaxHealth());
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameFullHealRangeAction tempAction = (GameFullHealRangeAction)toAdd;

        m_ranges.AddRange(tempAction.m_ranges);
    }

    public override void SubtractAction(GameAction toAdd)
    {
        GameFullHealRangeAction tempAction = (GameFullHealRangeAction)toAdd;

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
