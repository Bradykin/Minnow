using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTile
{
    private GameEntityBase m_occupyingEntity;

    public void PlaceEntity(GameEntityBase newEntity)
    {
        if (IsOccupied())
        {
            EngineLog.LogWarning("Placing new entity " + newEntity.m_name + " over existing entity " + m_occupyingEntity.m_name + ".");
        }

        m_occupyingEntity = newEntity;

        newEntity.m_curTile = this;

        EngineLog.LogInfo("Placing entity");
    }

    public void ClearEntity()
    {
        if (!IsOccupied())
        {
            EngineLog.LogWarning("Clearing entity on a tile, but no entity currently exists on this tile.");
        }

        m_occupyingEntity = null;

        EngineLog.LogInfo("Clearing entity");
    }

    public bool IsOccupied()
    {
        return m_occupyingEntity != null;
    }
}
