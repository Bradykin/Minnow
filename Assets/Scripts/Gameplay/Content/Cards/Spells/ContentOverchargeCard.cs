using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOverchargeCard : GameCardSpellBase
{
    public ContentOverchargeCard()
    {
        m_name = "Overcharge";
        m_desc = "Maximize the AP of target creation.";
        m_playDesc = "The creation gets energized!";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Rare;

        SetupBasicData();
    }

    public override bool IsValidToPlay(GameEntity targetEntity)
    {
        return base.IsValidToPlay() && targetEntity.GetTypeline() == Typeline.Creation;
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.FillAP();
    }
}
