﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBatteryPackCard : GameCardSpellBase
{
    public ContentBatteryPackCard()
    {
        m_spellEffect = 1;

        m_name = "Battery Pack";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.MaxStamina);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override string GetDesc()
    {
        return "Target allied <b>Creation</b> unit gains " + m_spellEffect + " (" + GetSpellValue() + ") max Stamina.\n" + GetModifiedBySpellPowerString();
    }

    public override bool IsValidToPlay(GameUnit targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Creation;
    }

    public override void PlayCard(GameUnit targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddMaxStamina(GetSpellValue());
    }
}