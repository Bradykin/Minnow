using Game.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnemyEntity : GameEntity, ITakeTurnAI
{
    public AIGameEnemyEntity AIGameEnemyEntity;

    public GameEnemyEntity()
    {
        AIGameEnemyEntity = new AIGameEnemyEntity(this);
    }

    //============================================================================================================//

    public void TakeTurn()
    {
        List<WorldTile> tilesInRange = WorldGridManager.Instance.GetSurroundingTiles(WorldGridManager.Instance.GetWorldGridTileAtPosition(m_curTile.m_gridPosition), GetRange());

        List<GameEntity> possibleEntityTargets = new List<GameEntity>();
        List<GameBuildingBase> possibleBuildingTargets = new List<GameBuildingBase>();

        foreach (var tile in tilesInRange)
        {
            if (tile.m_gameTile.m_building != null)
            {

            }

            if (tile.m_gameTile.m_occupyingEntity != null && tile.m_gameTile.m_occupyingEntity.GetTeam() == Team.Enemy && CanHitEntity(tile.m_gameTile.m_occupyingEntity))
            {

            }
        }

        /*GameTile randomAdjacentTile = m_curTile.RandomAdjacentTile();

        while (CanMoveTo(randomAdjacentTile))
        {
            MoveTo(randomAdjacentTile);
            randomAdjacentTile = m_curTile.RandomAdjacentTile();

            break;
        }*/
    }
}
