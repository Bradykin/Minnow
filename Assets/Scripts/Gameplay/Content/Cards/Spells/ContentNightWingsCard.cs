using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentNightWingsCard : GameCardSpellBase
{
    public ContentNightWingsCard()
    {
        m_name = "Night Wings";
        m_desc = "Give a friendly unit <b>Flying</b>, but it loses 1 Stamina regen.";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_keywordHolder.AddKeyword(new GameFlyingKeyword());

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Explorer);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddKeyword(new GameFlyingKeyword());
        targetUnit.AddStaminaRegen(-1);
    }
}