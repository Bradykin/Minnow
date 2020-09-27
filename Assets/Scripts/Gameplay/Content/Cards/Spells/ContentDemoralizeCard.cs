﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemoralizeCard : GameCardSpellBase
{
    public ContentDemoralizeCard()
    {
        m_name = "Demoralize";
        m_desc = "Set an entities AP to 2.";
        m_playDesc = "The target gets drained!";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Common;

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
        targetEntity.GainAP(2);
    }
}