using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentQuickStrikesCard : GameCardSpellBase
{
    public ContentQuickStrikesCard()
    {
        m_name = "Quick Strikes";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit only takes 1 stamina to attack.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.SetStaminaToAttack(1);
    }
}
