using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentRoarOfVictoryCard : GameCardSpellBase
{
    public ContentRoarOfVictoryCard()
    {
        m_name = "Roar of Victory";
        m_desc = "Target allied <b>Monster</b> unit gains '<b>Victorious</b>: Trigger all <b>Momentum</b> and <b>Enrage</b> keywords on this unit'.";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Rare;
        m_shouldExile = true;

        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(null));
        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(null));
        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.Momentum);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
    }

    public override bool IsValidToPlay(GameEntity targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Monster;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameVictoriousKeyword(new GameRoarOfVictoryAction(targetEntity)));
    }
}