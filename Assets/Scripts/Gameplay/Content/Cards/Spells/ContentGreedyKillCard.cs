﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGreedyKillCard : GameCardSpellBase
{
    private int m_goldGain = 20;

    public ContentGreedyKillCard()
    {
        m_spellEffect = 2;

        m_name = "Greedy Kill";
        m_targetType = Target.Enemy;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.MagicPower);
        m_tags.AddTag(GameTag.TagType.Gold);

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Deal " + m_spellEffect + mpString + " damage to target enemy. If it dies, gain " + m_goldGain + " gold.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        if (targetUnit.m_isDead)
        {
            GameHelper.GetPlayer().m_wallet.AddGold(m_goldGain);
        }
    }
}