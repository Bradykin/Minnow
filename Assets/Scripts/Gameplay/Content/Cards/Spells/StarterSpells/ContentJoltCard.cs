using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJoltCard : GameCardSpellBase
{
    public ContentJoltCard()
    {
        m_name = "Jolt";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;

        m_cost = 2;
        m_spellEffect = 1;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.Energize;
    }

    public override string GetDesc()
    {
        string description = "Target allied unit gains +" + m_spellEffect + " Stamina.";

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

        targetUnit.GainStamina(m_spellEffect);

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }
}
