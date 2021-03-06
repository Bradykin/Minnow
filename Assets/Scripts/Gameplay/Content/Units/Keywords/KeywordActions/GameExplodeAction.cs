﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameExplodeAction : GameAction
{
    private GameUnit m_explodingUnit;
    private int m_explodeAttack;
    private List<int> m_explodeRanges;

    public GameExplodeAction(GameUnit explodingUnit, int explodeAttack, int explodeRange)
    {
        m_explodingUnit = explodingUnit;
        m_explodeAttack = explodeAttack;
        m_explodeRanges = new List<int>();
        m_explodeRanges.Add(explodeRange);

        m_name = "Explode";
        m_actionParamType = ActionParamType.UnitIntListIntParam;
    }

    public GameExplodeAction(GameUnit explodingUnit, int explodeAttack, List<int> explodeRanges)
    {
        m_explodingUnit = explodingUnit;
        m_explodeAttack = explodeAttack;
        m_explodeRanges = new List<int>();
        m_explodeRanges.AddRange(explodeRanges);

        m_name = "Explode";
        m_actionParamType = ActionParamType.UnitIntListIntParam;
    }

    public override string GetDesc()
    {
        return "Explode for " + m_explodeAttack + " damage to all units and buildings in range " + m_explodeRanges.Max();
    }

    public override void DoAction()
    {
        GameHelper.GetGameController().AddIntermissionLock();

        List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(m_explodingUnit.GetGameTile(), m_explodeRanges.Max(), 1);

        for (int i = 0; i < surroundingTiles.Count; i++)
        {
            GameBuildingBase building = surroundingTiles[i].GetBuilding();
            GameUnit unit = surroundingTiles[i].GetOccupyingUnit();

            if (building != null && !building.m_isDestroyed)
            {
                building.GetHit(m_explodeAttack);
            }

            if (unit != null && !unit.m_isDead)
            {
                unit.GetHitByAbility(m_explodeAttack);
            }
        }

        if (Globals.m_selectedUnit != null && Globals.m_selectedUnit.GetUnit() == m_explodingUnit)
        {
            Globals.m_selectedUnit = null;
        }

        AudioHelper.PlaySFX(AudioHelper.LargeImpact);

        GameHelper.GetGameController().RemoveIntermissionLock();
    }

    public override void AddAction(GameAction toAdd)
    {
        GameExplodeAction tempAction = (GameExplodeAction)toAdd;

        m_explodeAttack += tempAction.m_explodeAttack;
        m_explodeRanges.AddRange(tempAction.m_explodeRanges);
    }

    public override void SubtractAction(GameAction toAdd)
    {
        GameExplodeAction tempAction = (GameExplodeAction)toAdd;

        m_explodeAttack -= tempAction.m_explodeAttack;
        for (int i = 0; i < tempAction.m_explodeRanges.Count; i++)
        {
            m_explodeRanges.Remove(tempAction.m_explodeRanges[i]);
        }
    }

    public override bool ShouldBeRemoved()
    {
        return m_explodeAttack <= 0 || m_explodeRanges.Count == 0;
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
            intValue1 = m_explodeAttack,
            intListValue1 = m_explodeRanges
        };

        return jsonData;
    }

    public override void LoadFromJson(JsonGameActionData jsonData)
    {
        //Currently nothing needs to be done here
    }
}
