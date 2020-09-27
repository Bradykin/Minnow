using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFuryCard : GameCardSpellBase
{
    public ContentFuryCard()
    {
        m_name = "Fury";
        m_desc = "Trigger all instances of Momentum, Enrage, and Victorious on target ally monster.";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Momentum);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Victorious);
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

        List<GameMomentumKeyword> momentumKeywords = targetEntity.GetKeywords<GameMomentumKeyword>();
        List<GameEnrageKeyword> enrageKeywords = targetEntity.GetKeywords<GameEnrageKeyword>();
        List<GameVictoriousKeyword> victoriousKeywords = targetEntity.GetKeywords<GameVictoriousKeyword>();

        int numBestialWrath = GameHelper.RelicCount<ContentBestialWrathRelic>();

        for (int i = 0; i < momentumKeywords.Count; i++)
        {
            momentumKeywords[i].DoAction();
            for (int k = 0; k < numBestialWrath; k++)
            {
                momentumKeywords[i].DoAction();
            }
        }

        for (int i = 0; i < enrageKeywords.Count; i++)
        {
            enrageKeywords[i].DoAction(0);
            for (int k = 0; k < numBestialWrath; k++)
            {
                enrageKeywords[i].DoAction(0);
            }
        }

        for (int i = 0; i < victoriousKeywords.Count; i++)
        {
            victoriousKeywords[i].DoAction();
            for (int k = 0; k < numBestialWrath; k++)
            {
                victoriousKeywords[i].DoAction();
            }
        }
    }
}