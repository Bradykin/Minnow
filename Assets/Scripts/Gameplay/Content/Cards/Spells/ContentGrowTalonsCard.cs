﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrowTalonsCard : GameCardSpellBase
{
    int m_powerIncrease = 3;

    public ContentGrowTalonsCard()
    {
        m_name = "Grow Talons";
        m_desc = "Grant an ally +" + m_powerIncrease + " power.";
        m_playDesc = "The target grows talons.";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddPower(m_powerIncrease);
    }
}
