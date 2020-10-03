using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBullheadedCard : GameCardSpellBase
{
    public ContentBullheadedCard()
    {
        m_name = "Bullheaded";
        m_desc = "Target allied <b>Monster</b> unit gains '<b>Enrage</b>: +1/+0.'";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;

        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Healing);
    }

    public override bool IsValidToPlay(GameUnit targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Monster;
    }

    public override void PlayCard(GameUnit targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameEnrageKeyword(new GameGainPowerAction(targetEntity, 1)));
    }
}