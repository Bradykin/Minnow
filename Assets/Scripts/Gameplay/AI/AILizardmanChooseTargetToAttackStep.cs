using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILizardmanChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AILizardmanChooseTargetToAttackStep(AIGameEnemyEntity AIGameEnemyEntity) : base(AIGameEnemyEntity) { }

    public override void TakeStep()
    {
        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = castleInRange;
            return;
        }

        GameEntity closestEntityInRange = FindClosestEntityInRangeToWater();
        if (closestEntityInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestEntityInRange;
            return;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRange();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyEntity.m_targetGameElement = closestBuildingInRange;
            return;
        }

        m_AIGameEnemyEntity.m_targetGameElement = null;
    }

    protected GameEntity FindClosestEntityInRangeToWater()
    {
        if (m_AIGameEnemyEntity.m_possibleEntityTargets.Count == 1)
        {
            return m_AIGameEnemyEntity.m_possibleEntityTargets[0];
        }
        else if (m_AIGameEnemyEntity.m_possibleEntityTargets.Count > 1)
        {
            int minDistanceToWater = DistanceToWaterOnPath(m_AIGameEnemyEntity.m_possibleEntityTargets[0].m_curTile);
            int minTravelCost = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, m_AIGameEnemyEntity.m_possibleEntityTargets[0].m_curTile);
            int indexMinDistance = 0;
            for (int i = 1; i < m_AIGameEnemyEntity.m_possibleEntityTargets.Count; i++)
            {
                int currentDistanceToWater = DistanceToWaterOnPath(m_AIGameEnemyEntity.m_possibleEntityTargets[i].m_curTile);
                if (currentDistanceToWater < minDistanceToWater)
                {
                    minDistanceToWater = currentDistanceToWater;
                    minTravelCost = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, m_AIGameEnemyEntity.m_possibleEntityTargets[i].m_curTile);
                    indexMinDistance = i;
                }
                else if (currentDistanceToWater == minDistanceToWater)
                {
                    int currentTravelCost = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, m_AIGameEnemyEntity.m_possibleEntityTargets[i].m_curTile);
                    if (currentTravelCost < minTravelCost)
                    {
                        minTravelCost = currentTravelCost;
                        indexMinDistance = i;
                    }
                }
            }

            return m_AIGameEnemyEntity.m_possibleEntityTargets[indexMinDistance];
        }
        else
        {
            return null;
        }
    }

    //Trying two different approaches - first one being the distance on the path, second one being distance in general to a body of water
    protected int DistanceToWaterOnPath(GameTile gameTile)
    {
        List<GameTile> pathToTile = WorldGridManager.Instance.CalculateAStarPath(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile, gameTile, false, true, false);

        int distance = 0;
        for (int i = pathToTile.Count - 1; i >= 0; i--)
        {
            if (pathToTile[i].GetTerrain().IsWater())
            {
                return distance;
            }
            distance++;
        }

        return 9999;
    }

    protected int DistanceToWaterAround(GameTile gameTile)
    {
        if (gameTile.GetTerrain().IsWater())
        {
            return 0;
        }

        for (int i = 1; i < 5; i++)
        {
            List<GameTile> tilesAtDistance = WorldGridManager.Instance.GetSurroundingTiles(gameTile, i, i);
            for (int k = 0; k < tilesAtDistance.Count; k++)
            {
                if (tilesAtDistance[k].GetTerrain().IsWater())
                {
                    return i;
                }
            }
        }

        return 9999;
    }
}