using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentJoltCard : GameCardSpellBase
{
    public ContentJoltCard()
    {
        m_spellEffect = 1;

        m_name = "Jolt";
        m_desc = "Restore 1 AP.\nDraw a card.";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.APRegen);
        m_tags.AddTag(GameTag.TagType.Knowledgeable);
        m_tags.AddTag(GameTag.TagType.Spellpower);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override string GetDesc()
    {
        return "Restore " + m_spellEffect + GetSpellPowerString() + " AP.\nDraw a card.\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GainAP(GetSpellValue());

        GameHelper.GetPlayer().DrawCard();
    }
}
