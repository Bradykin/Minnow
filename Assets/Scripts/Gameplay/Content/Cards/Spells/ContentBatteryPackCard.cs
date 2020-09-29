using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBatteryPackCard : GameCardSpellBase
{
    public ContentBatteryPackCard()
    {
        m_spellEffect = 1;

        m_name = "Battery Pack";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.MaxAP);
        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override string GetDesc()
    {
        return "Target allied Creation unit gains " + m_spellEffect + " (" + GetSpellValue() + ") max AP.\n" + GetModifiedBySpellPowerString();
    }

    public override bool IsValidToPlay(GameEntity targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Creation;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddMaxAP(GetSpellValue());
    }
}