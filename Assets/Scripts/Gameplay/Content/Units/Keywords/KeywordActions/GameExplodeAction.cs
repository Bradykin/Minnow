using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameExplodeAction : GameAction
{
    private GameUnit m_explodingUnit;
    private int m_explodePower;
    private List<int> m_explodeRanges;

    public GameExplodeAction(GameUnit explodingUnit, int explodePower, int explodeRange)
    {
        m_explodingUnit = explodingUnit;
        m_explodePower = explodePower;
        m_explodeRanges = new List<int>();
        m_explodeRanges.Add(explodeRange);

        m_name = "Explode";
        m_actionParamType = ActionParamType.UnitIntListIntParam;
    }

    public override string GetDesc()
    {
        return "Explode for " + m_explodePower + " damage to all units and buildings in range " + m_explodeRanges.Max();
    }

    public override void DoAction()
    {
        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_explodingUnit.GetGameTile(), m_explodeRanges.Max(), 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameBuildingBase building = surroundingTiles[i].GetBuilding();
            GameUnit unit = surroundingTiles[i].m_occupyingUnit;

            if (building != null && !building.m_isDestroyed)
            {
                building.GetHit(m_explodePower);
            }

            if (unit != null && !unit.m_isDead)
            {
                unit.GetHit(m_explodePower);
            }
        }
    }

    public override void AddAction(GameAction toAdd)
    {
        GameExplodeAction tempAction = (GameExplodeAction)toAdd;

        m_explodePower += tempAction.m_explodePower;
        m_explodeRanges.AddRange(tempAction.m_explodeRanges);
    }

    public override void SubtractAction(GameAction toAdd)
    {
        GameExplodeAction tempAction = (GameExplodeAction)toAdd;

        m_explodePower -= tempAction.m_explodePower;
        for (int i = 0; i < tempAction.m_explodeRanges.Count; i++)
        {
            m_explodeRanges.Remove(tempAction.m_explodeRanges[i]);
        }
    }

    public override bool ShouldBeRemoved()
    {
        return m_explodePower <= 0 || m_explodeRanges.Count == 0;
    }

    public override JsonActionData SaveToJson()
    {
        JsonActionData jsonData = new JsonActionData
        {
            name = m_name,
            intValue1 = m_explodePower,
            intListValue1 = m_explodeRanges
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
