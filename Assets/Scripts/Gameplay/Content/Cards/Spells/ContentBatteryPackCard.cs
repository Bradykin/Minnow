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

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Target allied <b>Creation</b> unit gains " + m_spellEffect + mpString + " max Stamina.\n" + GetModifiedByMagicPowerString();
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetTypeline() == Typeline.Creation;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddMaxStamina(GetSpellValue(), false);
    }
}