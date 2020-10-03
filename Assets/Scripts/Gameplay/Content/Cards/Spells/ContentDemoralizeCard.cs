using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemoralizeCard : GameCardSpellBase
{
    public ContentDemoralizeCard()
    {
        m_name = "Demoralize";
        m_desc = "Set a units Stamina to 2.";
        m_targetType = Target.Unit;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.UtilitySpell);
        m_tags.AddTag(GameTag.TagType.DamageSpell);
    }

    public override void PlayCard(GameUnit targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.EmptyStamina();
        targetEntity.GainStamina(2);
    }
}
