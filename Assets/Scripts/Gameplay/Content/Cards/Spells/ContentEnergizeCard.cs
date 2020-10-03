using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnergizeCard : GameCardSpellBase
{
    public ContentEnergizeCard()
    {
        m_name = "Energize";
        m_desc = "Maximize target units Stamina.";
        m_targetType = Target.Unit;
        m_cost = 2;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override void PlayCard(GameUnit targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.FillStamina();
    }
}
