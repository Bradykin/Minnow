using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnergizeCard : GameCardSpellBase
{
    public ContentEnergizeCard()
    {
        m_name = "Energize";
        m_desc = "Maximize an entities AP.";
        m_targetType = Target.Entity;
        m_cost = 2;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.APRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.FillAP();
    }
}
