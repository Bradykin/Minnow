using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGainStaminaRangeAction : GameAction
{
    private GameUnit m_unit;
    private int m_toGain;
    private List<int> m_ranges;

    public GameGainStaminaRangeAction(GameUnit unit, int toGain, int range)
    {
        m_unit = unit;
        m_toGain = toGain;
        m_ranges = new List<int>();
        m_ranges.Add(range);

        m_name = "Gain Stamina Range";
        m_actionParamType = ActionParamType.UnitIntListIntParam;
    }

    public GameGainStaminaRangeAction(GameUnit unit, int toGain, List<int> ranges)
    {
        m_unit = unit;
        m_toGain = toGain;
        m_ranges = new List<int>();
        m_ranges.AddRange(ranges);

        m_name = "Gain Stamina Range";
        m_actionParamType = ActionParamType.UnitIntListIntParam;
    }

    public override string GetDesc()
    {
        return "All friendly units within range " + m_ranges.Max() + " gain " + m_toGain + " Stamina";
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_ranges.Max(), 0);
        
        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].GetOccupyingUnit().m_isDead && tilesInRange[i].GetOccupyingUnit().GetTeam() == m_unit.GetTeam())
            {
                tilesInRange[i].GetOccupyingUnit().GainStamina(m_toGain);
                AudioHelper.PlaySFX(AudioHelper.SmallBuff);
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainStaminaRangeAction tempAction = (GameGainStaminaRangeAction)toAdd;

        m_toGain += tempAction.m_toGain;
        m_ranges.AddRange(tempAction.m_ranges);
    }

    public override void SubtractAction(GameAction toAdd)
    {
        GameGainStaminaRangeAction tempAction = (GameGainStaminaRangeAction)toAdd;

        m_toGain -= tempAction.m_toGain;
        for (int i = 0; i < tempAction.m_ranges.Count; i++)
        {
            m_ranges.Remove(tempAction.m_ranges[i]);
        }
    }

    public override bool ShouldBeRemoved()
    {
        return m_toGain <= 0 || m_ranges.Count == 0;
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
            intValue1 = m_toGain,
            intListValue1 = m_ranges
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
