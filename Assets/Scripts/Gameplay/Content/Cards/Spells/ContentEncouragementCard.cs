﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEncouragementCard : GameCardSpellBase
{
    public ContentEncouragementCard()
    {
        m_name = "Encouragement";
        m_desc = "Deal 1 damage to target ally monster, then give them +3 maximum health.";
        m_playDesc = "Sometimes pain is a good teacher.";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override bool IsValidToPlay(GameEntity targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Monster;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GetHit(1);

        if (targetEntity.m_isDead)
        {
            return;
        }

        targetEntity.AddMaxHealth(3);
    }
}