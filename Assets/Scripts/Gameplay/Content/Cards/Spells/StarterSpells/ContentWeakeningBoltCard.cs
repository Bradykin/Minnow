﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWeakeningBoltCard : GameCardSpellBase
{
    public ContentWeakeningBoltCard()
    {
        m_name = "Weakening Bolt";
        m_targetType = Target.Unit;
        m_rarity = GameRarity.Starter;

        InitializeWithLevel(GetCardLevel());

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Damage;
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString(false) + "Drain " + m_spellEffect + " Power from the target.\n";

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            description += "Draw a card.";
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

        targetUnit.GetHit(m_spellEffect);
        targetUnit.RemoveStats(m_spellEffect, 0);

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;
        m_spellEffect = 3;

        if (level >= 1)
        {
            m_cost = 0;
        }

        if (level >= 2)
        {
            m_spellEffect = 5;
        }
    }
}
