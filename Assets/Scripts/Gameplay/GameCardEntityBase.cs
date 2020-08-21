using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCardEntityBase : GameCard
{
    public GameEntity m_entity { get; protected set; }

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

        GameHelper.MakePlayerEntity(targetTile, m_entity);
    }

    public override bool IsValidToPlay(GameTile targetTile)
    {
        if (!base.IsValidToPlay(targetTile))
        {
            return false;
        }

        if (targetTile.IsOccupied())
        {
            return false;
        }

        return true;
    }
}
