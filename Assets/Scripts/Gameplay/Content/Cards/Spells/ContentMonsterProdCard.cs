using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMonsterProdCard : GameCardSpellBase
{
    public ContentMonsterProdCard()
    {
        m_name = "Monster Prod";
        m_desc = "Target ally monster gets Enrage: Gain 1 AP.";
        m_playDesc = "Every monster needs a push once in a while.";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

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

        base.PlayCard(targetEntity);

        targetEntity.AddKeyword(new GameEnrageKeyword(new GameGainAPAction(targetEntity, 1)));
    }
}