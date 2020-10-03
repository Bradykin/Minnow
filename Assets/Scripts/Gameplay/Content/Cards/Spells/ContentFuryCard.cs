using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFuryCard : GameCardSpellBase
{
    public ContentFuryCard()
    {
        m_name = "Fury";
        m_desc = "Trigger all instances of <b>Momentum</b>, <b>Enrage</b>, and <b>Victorious</b> on target allied <b>Monster</b>.";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        m_keywordHolder.m_keywords.Add(new GameMomentumKeyword(null));
        m_keywordHolder.m_keywords.Add(new GameEnrageKeyword(null));
        m_keywordHolder.m_keywords.Add(new GameVictoriousKeyword(null));

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Monster);
        m_tags.AddTag(GameTag.TagType.Momentum);
        m_tags.AddTag(GameTag.TagType.Enrage);
        m_tags.AddTag(GameTag.TagType.Victorious);
        m_tags.AddTag(GameTag.TagType.BuffSpell);
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