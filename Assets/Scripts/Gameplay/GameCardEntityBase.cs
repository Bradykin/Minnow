using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardEntityBase : GameCard
{
    protected GameEntity m_entity { get; set; }

    public GameEntity GetEntity()
    {
        return m_entity;
    }

    public override void PlayCard(GameTile targetTile)
    {
        if (!IsValidToPlay(targetTile))
        {
            return;
        }

        targetTile.PlaceEntity(m_entity);
    }

    public override bool IsValidToPlay(GameTile targetTile)
    {
        if (targetTile.IsOccupied())
        {
            return false;
        }

        return true;
    }
}
