using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentDemoncAspectCard : GameCardSpellBase
{
    public ContentDemoncAspectCard()
    {
        m_name = "Demonic Aspect";
        m_desc = "Give an entity Victorious: Fill AP.";
        m_playDesc = "The target gains a demonic visage!";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_shouldExile = true;

        m_rarity = GameRarity.Rare;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GetKeywordHolder().m_keywords.Add(new GameVictoriousKeyword(new GameGainAPAction(targetEntity, targetEntity.GetMaxAP())));
    }
}