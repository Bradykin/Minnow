﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoblinCard : GameCardEntityBase
{
    public GameGoblinCard()
    {
        m_name = "Goblin";
        m_desc = "A basic goblin, a simple troop.";
        m_typeline = "Summon - Goblin";
        m_cost = 1;
        m_icon = null;

        m_entity = new GameGoblinEntity();
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