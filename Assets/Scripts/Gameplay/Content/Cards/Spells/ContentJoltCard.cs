using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJoltCard : GameCardSpellBase
{
    public ContentJoltCard()
    {
        m_spellEffect = 1;

        m_name = "Jolt";
        m_desc = "Restore 1 Stamina.\nDraw a card.";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override string GetDesc()
    {
        return "Target allied unit gains +" + m_spellEffect + GetSpellPowerString() + " Stamina.\nDraw a card.\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameUnit targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GainStamina(GetSpellValue());

        GameHelper.GetPlayer().DrawCard();
    }
}
