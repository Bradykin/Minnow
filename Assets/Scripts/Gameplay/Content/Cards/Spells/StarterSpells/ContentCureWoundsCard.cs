using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentCureWoundsCard : GameCardSpellBase
{
    public ContentCureWoundsCard()
    {
        m_name = "Cure Wounds";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;
        
        m_cost = 1;
        m_spellEffect = 6;

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string description = GetHealDescString();

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

        targetUnit.Heal(GetSpellValue());


        if (GameHelper.HasRelic<ContentTraditionalMethodsRelic>())
        {
            GameHelper.GetPlayer().DrawCard();
        }
    }
}
