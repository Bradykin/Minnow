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
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.MaxStamina);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.SciFiBuffSmall;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gains {UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} max Stamina.";
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