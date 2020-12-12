using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameExplodeEnemiesAction : GameAction
{
    private GameUnit m_explodingUnit;
    private int m_explodePower;
    private List<int> m_explodeRanges;

    public GameExplodeEnemiesAction(GameUnit explodingUnit, int explodePower, int explodeRange)
    {
        m_explodingUnit = explodingUnit;
        m_explodePower = explodePower;
        m_explodeRanges = new List<int>();
        m_explodeRanges.Add(explodeRange);

        m_name = "Explode Enemies";
        m_actionParamType = ActionParamType.UnitIntListIntParam;
    }

    public GameExplodeEnemiesAction(GameUnit explodingUnit, int explodePower, List<int> explodeRanges)
    {
        m_explodingUnit = explodingUnit;
        m_explodePower = explodePower;
        m_explodeRanges = new List<int>();
        m_explodeRanges.AddRange(explodeRanges);

        m_name = "Explode Enemies";
        m_actionParamType = ActionParamType.UnitIntListIntParam;
    }

    public override string GetDesc()
    {
        return "Explode for " + m_explodePower + " damage to enemy units and enemy buildings in range " + m_explodeRanges.Max();
    }

    public override void DoAction()
    {
        GameHelper.GetGameController().AddIntermissionLock();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_explodingUnit.GetGameTile(), m_explodeRanges.Max(), 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameBuildingBase building = surroundingTiles[i].GetBuilding();
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (building != null && !building.m_isDestroyed && building.GetTeam() == Team.Enemy)
            {
                building.GetHit(m_explodePower);
            }

            if (unit != null && !unit.m_isDead && unit.GetTeam() == Team.Enemy)
            {
                unit.GetHitByAbility(m_explodePower);
            }
        }

        if (Globals.m_selectedUnit != null && Globals.m_selectedUnit.GetUnit() == m_explodingUnit)
        {
            Globals.m_selectedUnit = null;
        }

        GameHelper.GetGameController().RemoveIntermissionLock();
    }

    public override void AddAction(GameAction toAdd)
    {
        GameExplodeEnemiesAction tempAction = (GameExplodeEnemiesAction)toAdd;

        m_explodePower += tempAction.m_explodePower;
        m_explodeRanges.AddRange(tempAction.m_explodeRanges);
    }

    public override void SubtractAction(GameAction toAdd)
    {
        GameExplodeEnemiesAction tempAction = (GameExplodeEnemiesAction)toAdd;

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

    public override GameUnit GetGameUnit()
    {
        return m_explodingUnit;
    }

    public override JsonGameActionData SaveToJson()
    {
        JsonGameActionData jsonData = new JsonGameActionData
        {
            name = m_name,
            intValue1 = m_explodePower,
            intListValue1 = m_explodeRanges
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
