﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrainCard : GameCardSpellBase
{
    public ContentDrainCard()
    {
        m_name = "Drain";
        m_desc = "Set an entities AP to 0.";
        m_playDesc = "The target gets drained!";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.EmptyAP();
    }
}
