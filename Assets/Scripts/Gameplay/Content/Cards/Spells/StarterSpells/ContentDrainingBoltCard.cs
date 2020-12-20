using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDrainingBoltCard : GameCardSpellBase
{
    private int m_staminaToDrain = 1;

    public ContentDrainingBoltCard()
    {
        m_name = "Draining Bolt";
        m_targetType = Target.Unit;
        m_rarity = GameRarity.Starter;

        m_cost = 1;
        m_spellEffect = 1;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.MagicBolt;
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString() + "Drain " + m_staminaToDrain + " Stamina.\n";

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

        targetUnit.GetHitBySpell(GetSpellValue(), this);

        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }

        targetUnit.DrainStamina(m_staminaToDrain);
    }
}
