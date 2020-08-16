using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile
{
    public GameEntityBase m_occupyingEntity { get; private set; }
    public Vector2Int m_gridPosition;
    public GameTerrainBase m_terrain { get; private set; }

    public GameTile()
    {
        m_terrain = new GameGrassTerrain();
    }

    public void PlaceEntity(GameEntityBase newEntity)
    {
        if (IsOccupied())
        {
            Debug.LogWarning("Placing new entity " + newEntity.m_name + " over existing entity " + m_occupyingEntity.m_name + ".");
        }

        m_occupyingEntity = newEntity;
        newEntity.m_curTile = this;
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
}
