using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentFuryCard : GameCardSpellBase
{
    public ContentFuryCard()
    {
        m_name = "Fury";
        m_desc = "Trigger all instances of Momentum, Enrage, and Victorious on target ally monster.";
        m_playDesc = "Fuuurryyyyy!";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
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

        base.PlayCard();

        List<GameMomentumKeyword> momentumKeywords = targetEntity.GetKeywords<GameMomentumKeyword>();
        List<GameEnrageKeyword> enrageKeywords = targetEntity.GetKeywords<GameEnrageKeyword>();
        List<GameVictoriousKeyword> victoriousKeywords = targetEntity.GetKeywords<GameVictoriousKeyword>();

        for (int i = 0; i < momentumKeywords.Count; i++)
        {
            momentumKeywords[i].DoAction();
        }

        for (int i = 0; i < enrageKeywords.Count; i++)
        {
            enrageKeywords[i].DoAction();
        }

        for (int i = 0; i < victoriousKeywords.Count; i++)
        {
            victoriousKeywords[i].DoAction();
        }
    }
}