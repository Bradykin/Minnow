using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameGainKeywordRangeAction : GameAction
{
    private GameUnit m_unit;
    private List<int> m_ranges;
    private GameKeywordBase m_keyword;

    public GameGainKeywordRangeAction(GameUnit unit, int range, GameKeywordBase keyword)
    {
        m_unit = unit;
        m_ranges = new List<int>();
        m_ranges.Add(range);
        m_keyword = keyword;

        m_name = "Add Keyword Range";
        m_actionParamType = ActionParamType.UnitListIntKeywordParam;
    }

    public GameGainKeywordRangeAction(GameUnit unit, List<int> ranges, GameKeywordBase keyword)
    {
        m_unit = unit;
        m_ranges = new List<int>();
        m_ranges.AddRange(ranges);
        m_keyword = keyword;

        m_name = "Add Keyword Range";
        m_actionParamType = ActionParamType.UnitListIntKeywordParam;
    }

    public override string GetDesc()
    {
        return $"Add {m_keyword.GetDesc()} {m_keyword.GetName()} to all allied units in range {m_ranges.Max()}";
    }

    public override void DoAction()
    {
        List<GameTile> tilesInRange = WorldGridManager.Instance.GetSurroundingGameTiles(m_unit.GetGameTile(), m_ranges.Max(), 0);
        
        for (int i = 0; i < tilesInRange.Count; i++)
        {
            if (tilesInRange[i].IsOccupied() && !tilesInRange[i].m_occupyingUnit.m_isDead && tilesInRange[i].m_occupyingUnit.GetTeam() == m_unit.GetTeam())
            {
                tilesInRange[i].m_occupyingUnit.AddKeyword(m_keyword);
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameGainKeywordRangeAction tempAction = (GameGainKeywordRangeAction)toAdd;

        m_keyword.AddKeyword(tempAction.GetKeyword());
    }

    public override void SubtractAction(GameAction toSubtract)
    {
        GameGainKeywordRangeAction tempAction = (GameGainKeywordRangeAction)toSubtract;

        m_keyword.SubtractKeyword(tempAction.GetKeyword());
    }

    public GameKeywordBase GetKeyword()
    {
        return m_keyword;
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
            intListValue1 = m_ranges,
            gameKeywordData = m_keyword.SaveToJson()
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
