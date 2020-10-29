﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWeakeningBoltCard : GameCardSpellBase
{
    private int m_drainPower = 1;

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
        string description = GetDamageDescString() + "Drain " + m_drainPower + " Power, adding it to the damage of this card.\n";

        int numTraditionalMethods = GameHelper.RelicCount<ContentTraditionalMethodsRelic>();

        if (numTraditionalMethods > 1)
        {
            description += "Draw " + numTraditionalMethods + " cards.";
        }
        else if (numTraditionalMethods > 0)
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

        targetUnit.GetHit(GetSpellValue());

        int numTraditionalMethods = GameHelper.RelicCount<ContentTraditionalMethodsRelic>();
        for (int i = 0; i < numTraditionalMethods; i++)
        {
            GameHelper.GetPlayer().DrawCard();
        }

        targetUnit.RemoveStats(m_drainPower, 0);

        m_spellEffect += m_drainPower;
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;
        m_spellEffect = 2;

        if (level >= 1)
        {
            m_cost = 0;
        }

        if (level >= 2)
        {
            m_drainPower = 2;
        }
    }
}
