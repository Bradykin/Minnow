using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile
{
    public GameEntity m_occupyingEntity { get; private set; } //Always set this with PlaceEntity() or ClearEntity() to ensure proper data setup
    public Vector2Int m_gridPosition;
    public GameTerrainBase m_terrain { get; private set; }
    public GameEvent m_event { get; private set; }
    public GameBuildingBase m_building { get; private set; }

    public GameTile()
    {
        m_terrain = new ContentGrassTerrain();

        if (GameHelper.PercentChanceRoll(Constants.PercentChanceForTileToContainEvent))
        {
            m_event = GameEventFactory.GetRandomEvent(this);
        }

        if (GameHelper.PercentChanceRoll(Constants.PercentChanceForTileToContainBuilding))
        {
            GameHelper.MakePlayerBuilding(this, new ContentTowerBuilding());
        }
    }

    public void PlaceEntity(GameEntity newEntity)
    {
        if (IsOccupied())
        {
            Debug.LogWarning("Placing new entity " + newEntity.m_name + " over existing entity " + m_occupyingEntity.m_name + ".");
        }

        m_occupyingEntity = newEntity;
        newEntity.m_curTile = this;
    }

    public void PlaceBuilding(GameBuildingBase newBuilding)
    {
        if (HasBuilding())
        {
            Debug.LogWarning("Placing new building " + newBuilding.m_name + " over existing building " + m_building.m_name + ".");
        }

        m_building = newBuilding;
    }

    public void ClearEntity()
    {
        if (!IsOccupied())
        {
            Debug.LogWarning("Clearing entity on a tile, but no entity currently exists on this tile.");
        }

        m_occupyingEntity = null;
    }

    public bool IsOccupied()
    {
        return m_occupyingEntity != null;
    }

    public bool HasAvailableEvent()
    {
        return m_event != null;
    }

    public bool HasBuilding()
    {
        return m_building != null;
    }
}
