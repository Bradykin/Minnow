using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AILizardmanChooseTargetToAttackStep : AIChooseTargetToAttackStandardStep
{
    public AILizardmanChooseTargetToAttackStep(AIGameEnemyUnit AIGameEnemyUnit) : base(AIGameEnemyUnit) { }

    public override void TakeStepInstant()
    {
        GameUnit closestTauntUnitInRange = FindClosestTauntUnitInRange();
        if (closestTauntUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestTauntUnitInRange;
            return;
        }

        GameUnit closestVulnerableUnitInRange = FindClosestVulnerableUnitInRange();
        if (closestVulnerableUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableUnitInRange;
            return;
        }

        GameBuildingBase closestVulnerableBuildingInRange = FindClosestVulnerableBuildingInRange();
        if (closestVulnerableBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestVulnerableBuildingInRange;
            return;
        }

        GameUnit closestUnitInRange = FindClosestUnitInRangeToWater();
        if (closestUnitInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestUnitInRange;
            return;
        }

        GameBuildingBase castleInRange = FindCastleInRange();
        if (castleInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = castleInRange;
            return;
        }

        GameBuildingBase closestBuildingInRange = FindClosestBuildingInRangeToWater();
        if (closestBuildingInRange != null)
        {
            m_AIGameEnemyUnit.m_targetGameElement = closestBuildingInRange;
            return;
        }

        m_AIGameEnemyUnit.m_targetGameElement = null;
        m_AIGameEnemyUnit.m_targetGameTile = null;

        GameTile moveDestination = m_AIGameEnemyUnit.m_gameEnemyUnit.GetMoveTowardsDestination(GameHelper.GetPlayer().GetCastleGameTile(), Mathf.Min(m_AIGameEnemyUnit.m_gameEnemyUnit.GetCurStamina(), m_AIGameEnemyUnit.m_gameEnemyUnit.GetStaminaRegen()));
        m_AIGameEnemyUnit.m_targetGameTile = moveDestination;
    }

    protected GameUnit FindClosestUnitInRangeToWater()
    {
        if (m_AIGameEnemyUnit.m_possibleUnitTargets.Count == 1)
        {
            return m_AIGameEnemyUnit.m_possibleUnitTargets[0];
        }
        else if (m_AIGameEnemyUnit.m_possibleUnitTargets.Count > 1)
        {
            int minDistanceToWater = DistanceToWaterOnPath(m_AIGameEnemyUnit.m_possibleUnitTargets[0].GetGameTile());
            int minTravelCost = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), m_AIGameEnemyUnit.m_possibleUnitTargets[0].GetGameTile());
            int indexMinDistance = 0;
            for (int i = 1; i < m_AIGameEnemyUnit.m_possibleUnitTargets.Count; i++)
            {
                int currentDistanceToWater = DistanceToWaterOnPath(m_AIGameEnemyUnit.m_possibleUnitTargets[i].GetGameTile());
                if (currentDistanceToWater < minDistanceToWater)
                {
                    minDistanceToWater = currentDistanceToWater;
                    minTravelCost = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), m_AIGameEnemyUnit.m_possibleUnitTargets[i].GetGameTile(), false, true, false);
                    indexMinDistance = i;
                }
                else if (currentDistanceToWater == minDistanceToWater)
                {
                    int currentTravelCost = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), m_AIGameEnemyUnit.m_possibleUnitTargets[i].GetGameTile(), false, true, false);
                    if (currentTravelCost < minTravelCost)
                    {
                        minTravelCost = currentTravelCost;
                        indexMinDistance = i;
                    }
                }
            }

            return m_AIGameEnemyUnit.m_possibleUnitTargets[indexMinDistance];
        }
        else
        {
            return null;
        }
    }

    protected GameBuildingBase FindClosestBuildingInRangeToWater()
    {
        if (m_AIGameEnemyUnit.m_possibleBuildingTargets.Count == 1)
        {
            return m_AIGameEnemyUnit.m_possibleBuildingTargets[0];
        }
        else if (m_AIGameEnemyUnit.m_possibleBuildingTargets.Count > 1)
        {
            int minDistanceToWater = DistanceToWaterOnPath(m_AIGameEnemyUnit.m_possibleBuildingTargets[0].GetGameTile());
            int minTravelCost = WorldGridManager.Instance.CalculateAbsoluteDistanceBetweenPositions(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), m_AIGameEnemyUnit.m_possibleBuildingTargets[0].GetGameTile());
            int indexMinDistance = 0;
            for (int i = 1; i < m_AIGameEnemyUnit.m_possibleBuildingTargets.Count; i++)
            {
                int currentDistanceToWater = DistanceToWaterOnPath(m_AIGameEnemyUnit.m_possibleBuildingTargets[i].GetGameTile());
                if (currentDistanceToWater < minDistanceToWater)
                {
                    minDistanceToWater = currentDistanceToWater;
                    minTravelCost = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), m_AIGameEnemyUnit.m_possibleBuildingTargets[i].GetGameTile(), false, true, false);
                    indexMinDistance = i;
                }
                else if (currentDistanceToWater == minDistanceToWater)
                {
                    int currentTravelCost = WorldGridManager.Instance.GetPathLength(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), m_AIGameEnemyUnit.m_possibleBuildingTargets[i].GetGameTile(), false, true, false);
                    if (currentTravelCost < minTravelCost)
                    {
                        minTravelCost = currentTravelCost;
                        indexMinDistance = i;
                    }
                }
            }

            return m_AIGameEnemyUnit.m_possibleBuildingTargets[indexMinDistance];
        }
        else
        {
            return null;
        }
    }

    //Todo ashulman: decide between below. Trying two different approaches - first one being the distance on the path, second one being distance in general to a body of water
    protected int DistanceToWaterOnPath(GameTile gameTile)
    {
        List<GameTile> pathToTile = WorldGridManager.Instance.CalculateAStarPath(m_AIGameEnemyUnit.m_gameEnemyUnit.GetGameTile(), gameTile, false, true, false);

        if (pathToTile == null || pathToTile.Count == 0)
        {
            return 9999;
        }

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
            List<GameTile> tilesAtDistance = WorldGridManager.Instance.GetSurroundingGameTiles(gameTile, i, i);
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