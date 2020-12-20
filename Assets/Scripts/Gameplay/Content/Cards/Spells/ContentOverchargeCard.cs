using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverchargeCard : GameCardSpellBase
{
    public ContentOverchargeCard()
    {
        m_name = "Overcharge";
        m_desc = "Maximize the Stamina of target allied <b>Creation</b> unit.";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Rare;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.MetalBuff;
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetTypeline() == Typeline.Creation;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.FillStamina();
    }
}
