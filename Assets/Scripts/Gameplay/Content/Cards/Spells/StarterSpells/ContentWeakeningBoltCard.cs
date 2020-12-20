using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentWeakeningBoltCard : GameCardSpellBase
{
    public ContentWeakeningBoltCard()
    {
        m_name = "Weakening Bolt";
        m_targetType = Target.Unit;
        m_rarity = GameRarity.Starter;

        m_cost = 1;
        m_spellEffect = 1;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.MagicBolt;
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString(false) + "Drain " + m_spellEffect + " Power from the target. Does not scale with <b>Magic Power</b>.\n";

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

        targetUnit.GetHitBySpell(m_spellEffect, this);
        targetUnit.RemoveStats(m_spellEffect, 0, false);

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }
}
