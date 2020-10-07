﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrainingBoltCard : GameCardSpellBase
{
    private int m_staminaToDrain = 1;

    public ContentDrainingBoltCard()
    {
        m_spellEffect = 2;

        m_name = "Draining Bolt";
        m_targetType = Target.Unit;
        m_cost = 1;
        m_rarity = GameRarity.Starter;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString() + "Drain 1 Stamina.\n";

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

        targetUnit.DrainStamina(m_staminaToDrain);
    }
}
