using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILizardmanFleeToWaterStep : AIStep
{
    public AILizardmanFleeToWaterStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override IEnumerator TakeStepCoroutine()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile().GetTerrain().IsWater())
        {
            yield break;
        }

        List<GameTile> tilesAtDistance = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 2, 1);

        List<GameTile> tilesToFleeInWater = new List<GameTile>();
        List<GameTile> tilesToFleeInOpenWater = new List<GameTile>();
        for (int i = 0; i < tilesAtDistance.Count; i++)
        {
            if (tilesAtDistance[i].GetTerrain().IsWater() && (!tilesAtDistance[i].IsOccupied() || tilesAtDistance[i].GetOccupyingUnit().m_isDead))
            {
                int pathLength = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), tilesAtDistance[i], false, false, false);

                if (pathLength > 0)
                {
                    yield break;
                }
                
                tilesToFleeInWater.Add(tilesAtDistance[i]);

                List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(tilesAtDistance[i], 1, 1);
                bool isOpenWater = true;
                for (int k = 0; k < surroundingTiles.Count; k++)
                {
                    if (!surroundingTiles[k].GetTerrain().IsWater())
                    {
                        isOpenWater = false;
                        break;
                    }
                }

                if (isOpenWater)
                {
                    tilesToFleeInOpenWater.Add(tilesAtDistance[i]);
                }
            }
        }

        GameTile moveDestination;
        if (tilesToFleeInOpenWater.Count > 0)
        {
            moveDestination = tilesToFleeInOpenWater[Random.Range(0, tilesToFleeInOpenWater.Count)];
        }
        else if (tilesToFleeInWater.Count > 0)
        {
            moveDestination = tilesToFleeInWater[Random.Range(0, tilesToFleeInWater.Count)];
        }
        else
        {
            yield break;
        }

        int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), moveDestination, true, false, true);
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);

        if (Constants.SteppedOutEnemyTurnsCameraFollowMovement && moveDistance >= Constants.SteppedOutEnemyTurnsCameraFollowThreshold)
        {
            UICameraController.Instance.SmoothCameraTransitionToGameObject(m_AIGameEnemyUnit.m_gameEnemyUnit.GetWorldTile().gameObject);
            while (UICameraController.Instance.IsCameraSmoothing())
            {
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);
    }

    public override void TakeStepInstant()
    {
        if (m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile().GetTerrain().IsWater())
        {
            return;
        }

        List<GameTile> tilesAtDistance = WorldGridManager.Instance.GetSurroundingGameTiles(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), 2, 1);

        List<GameTile> tilesToFleeInWater = new List<GameTile>();
        List<GameTile> tilesToFleeInOpenWater = new List<GameTile>();
        for (int i = 0; i < tilesAtDistance.Count; i++)
        {
            if (tilesAtDistance[i].GetTerrain().IsWater() && (!tilesAtDistance[i].IsOccupied() || tilesAtDistance[i].GetOccupyingUnit().m_isDead))
            {
                int pathLength = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), tilesAtDistance[i], false, false, false);

                if (pathLength > 0)
                {
                    return;
                }

                tilesToFleeInWater.Add(tilesAtDistance[i]);

                List<GameTile> surroundingTiles = WorldGridManager.Instance.GetSurroundingGameTiles(tilesAtDistance[i], 1, 1);
                bool isOpenWater = true;
                for (int k = 0; k < surroundingTiles.Count; k++)
                {
                    if (!surroundingTiles[k].GetTerrain().IsWater())
                    {
                        isOpenWater = false;
                        break;
                    }
                }

                if (isOpenWater)
                {
                    tilesToFleeInOpenWater.Add(tilesAtDistance[i]);
                }
            }
        }

        GameTile moveDestination;
        if (tilesToFleeInOpenWater.Count > 0)
        {
            moveDestination = tilesToFleeInOpenWater[Random.Range(0, tilesToFleeInOpenWater.Count)];
        }
        else if (tilesToFleeInWater.Count > 0)
        {
            moveDestination = tilesToFleeInWater[Random.Range(0, tilesToFleeInWater.Count)];
        }
        else
        {
            return;
        }

        int moveDistance = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), moveDestination, true, false, true);
        m_AIGameEnemyUnit.m_gameEnemyUnit.m_worldUnit.MoveTo(moveDestination);
    }
}