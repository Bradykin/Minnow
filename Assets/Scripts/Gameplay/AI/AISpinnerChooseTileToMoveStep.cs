using Game.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISpinnerChooseTileToMoveStep : AIMoveStep
{
    public AISpinnerChooseTileToMoveStep(AIGameEnemyUnit AIGameEnemyEnemy) : base(AIGameEnemyEnemy) { }

    public override void TakeStep()
    {
        List<GameTile> tilesInMoveAttackRange = WorldGridManager.Instance.GetTilesInMovementRangeWithStaminaToAttack(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), false, false);

        List<GameTile> tilesWithMaxAdjacent = new List<GameTile>();
        int maxAdjacent = 0;
        for (int i = 0; i < tilesInMoveAttackRange.Count; i++)
        {
            if (tilesInMoveAttackRange[i].IsOccupied() && tilesInMoveAttackRange[i] != m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile())
            {
                continue;
            }

            List<GameTile> adjacentTiles = tilesInMoveAttackRange[i].AdjacentTiles();
            int numAdjacent = 0;
            for (int k = 0; k < adjacentTiles.Count; k++)
            {
                if (adjacentTiles[k].IsOccupied() && adjacentTiles[k].m_occupyingUnit.GetTeam() == Team.Player)
                {
                    numAdjacent++;
                }
                if (adjacentTiles[k].HasBuilding())
                {
                    numAdjacent++;
                }
            }

            if (numAdjacent > 0)
            {
                if (numAdjacent == maxAdjacent)
                {
                    tilesWithMaxAdjacent.Add(tilesInMoveAttackRange[i]);
                }
                else if (numAdjacent > maxAdjacent)
                {
                    tilesWithMaxAdjacent.Clear();
                    tilesWithMaxAdjacent.Add(tilesInMoveAttackRange[i]);
                    maxAdjacent = numAdjacent;
                }
            }
        }

        if (tilesWithMaxAdjacent.Count == 0)
        {
            m_AIGameEnemyUnit.m_targetGameTile = null;
            MoveTowardsCastle(m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen());
        }
        else if (tilesWithMaxAdjacent.Count == 1)
        {
            m_AIGameEnemyUnit.m_targetGameTile = tilesWithMaxAdjacent[0];
        }
        else
        {
            for (int i = 0; i < tilesWithMaxAdjacent.Count; i++)
            {
                List<GameTile> adjacentTiles = tilesInMoveAttackRange[i].AdjacentTiles();
                for (int k = 0; k < adjacentTiles.Count; k++)
                {
                    if (adjacentTiles[k].HasBuilding() && adjacentTiles[k].GetBuilding().m_name == "Castle")
                    {
                        m_AIGameEnemyUnit.m_targetGameTile = tilesWithMaxAdjacent[i];
                        return;
                    }
                }
            }

            m_AIGameEnemyUnit.m_targetGameTile = tilesWithMaxAdjacent[Random.Range(0, tilesWithMaxAdjacent.Count)];
        }
    }
}