using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpinnerAttackStep : AIStep
{
    public AISpinnerAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStep()
    {
        if (!m_AIGameEnemyUnit.m_gameEnemyUnit.HasStaminaToAttack())
        {
            return;
        }

        List<GameTile> adjacentTiles = m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile().AdjacentTiles();
        bool hasSpentStamina = false;

        for (int i = 0; i < adjacentTiles.Count; i++)
        {
            if (adjacentTiles[i].m_occupyingUnit != null && adjacentTiles[i].m_occupyingUnit.GetTeam() != Team.Enemy)
            {
                m_AIGameEnemyUnit.m_gameEnemyUnit.HitUnit(adjacentTiles[i].m_occupyingUnit, !hasSpentStamina);
                hasSpentStamina = true;
            }

            if (adjacentTiles[i].GetBuilding() != null)
            {
                m_AIGameEnemyUnit.m_gameEnemyUnit.HitBuilding(adjacentTiles[i].GetBuilding(), !hasSpentStamina);
                hasSpentStamina = true;
            }
        }
    }
}
