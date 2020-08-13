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
            EngineLog.LogWarning("Warning!  Placing new entity " + newEntity.m_name + " over existing entity " + m_occupyingEntity.m_name + ".");
        }

        m_occupyingEntity = newEntity;
    }

    public bool IsOccupied()
    {
        return m_occupyingEntity != null;
    }
}
