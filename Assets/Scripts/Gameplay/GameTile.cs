using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile
{
    public GameEntityBase m_occupyingEntity { get; private set; }

    public void PlaceEntity(GameEntityBase newEntity)
    {
        if (IsOccupied())
        {
            EngineLog.LogWarning("Placing new entity " + newEntity.m_name + " over existing entity " + m_occupyingEntity.m_name + ".");
        }

        m_occupyingEntity = newEntity;
    }

    public void ClearEntity()
    {
        if (!IsOccupied())
        {
            EngineLog.LogWarning("Clearing entity on a tile, but no entity currently exists on this tile.");
        }

        m_occupyingEntity = null;
    }

    public bool IsOccupied()
    {
        return m_occupyingEntity != null;
    }
}
