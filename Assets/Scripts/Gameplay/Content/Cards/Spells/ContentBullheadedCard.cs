using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBullheadedCard : GameCardSpellBase
{
    public ContentBullheadedCard()
    {
        m_name = "Bullheaded";
        m_desc = "Target ally monster gains Enrage: +1 power.";
        m_playDesc = "Bulls are great role models!";
        m_targetType = Target.Ally;
        m_cost = 2;
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

        targetEntity.AddKeyword(new GameEnrageKeyword(new GameGainPowerAction(targetEntity, 1)));
    }
}