using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFireboltCard : GameCardSpellBase
{
    public ContentFireboltCard()
    {
        m_name = "Firebolt";
        m_targetType = Target.Enemy;
        m_rarity = GameRarity.Starter;

        m_cost = 1;
        m_spellEffect = 4;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.FireBlast;
    }

    public override string GetDesc()
    {
        string description = GetDamageDescString();

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
    }
}
