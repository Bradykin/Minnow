﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrowTalonsCard : GameCardSpellBase
{
    public ContentGrowTalonsCard()
    {
        m_name = "Grow Talons";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        m_cost = 1;
        m_spellEffect = 2;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        string description = $"<b>Permanently</b> give an allied unit +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}/+0.\n";
        
        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            description += "\nDraw a card.";
        }

        return description;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(GetSpellValue(), 0, true, true);
    }
}
