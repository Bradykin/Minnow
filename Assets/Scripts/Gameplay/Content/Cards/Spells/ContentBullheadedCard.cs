﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBullheadedCard : GameCardSpellBase
{
    public ContentBullheadedCard()
    {
        m_name = "Bullheaded";
        m_desc = "Target allied <b>Monster</b> unit gains '<b>Enrage</b>: +1/+0.'";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        m_playerUnlockLevel = 2;

        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Healing);
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay() && targetUnit.GetTypeline() == Typeline.Monster;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameEnrageKeyword(new GameGainPowerAction(targetUnit, 1)));
    }
}