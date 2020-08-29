using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScanTargetsInRangeStep : AIStep
{
    public override void TakeStep()
    {
        List<WorldTile> tilesInRange = WorldGridManager.Instance.GetSurroundingTiles(
            WorldGridManager.Instance.GetWorldGridTileAtPosition(m_AIGameEnemyEntity.m_gameEnemyEntity.m_curTile.m_gridPosition), 
            m_AIGameEnemyEntity.m_gameEnemyEntity.GetRange());

        List<GameEntity> possibleEntityTargets = new List<GameEntity>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInRange)
        {
            if (tile.GetGameTile().m_occupyingEntity != null && tile.GetGameTile().m_occupyingEntity.GetTeam() == Team.Enemy && m_AIGameEnemyEntity.m_gameEnemyEntity.CanHitEntity(tile.GetGameTile().m_occupyingEntity))
            {
                possibleEntityTargets.Add(tile.GetGameTile().m_occupyingEntity);
            }

            if (tile.GetGameTile().HasBuilding())
            {
                possibleBuildingTargets.Add(tile.GetGameTile().GetBuilding());
            }
        }

        m_AIGameEnemyEntity.m_possibleEntityTargets = possibleEntityTargets;
        m_AIGameEnemyEntity.m_possibleBuildingTargets = possibleBuildingTargets;
    }
}
