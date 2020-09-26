using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBloodMoneyCard : GameCardSpellBase
{
    public ContentBloodMoneyCard()
    {
        m_name = "Blood Money";
        m_desc = "Target ally gains Enrage: Gain gold equal to the damage taken.";
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

        targetEntity.AddKeyword(new GameEnrageKeyword(new GameGainGoldEnrageAction(targetEntity)));
    }
}