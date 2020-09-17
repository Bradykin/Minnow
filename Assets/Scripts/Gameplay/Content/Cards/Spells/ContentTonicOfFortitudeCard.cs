using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfFortitudeCard : GameCardSpellBase
{
    public ContentTonicOfFortitudeCard()
    {
        m_name = "Tonic of Fortitude";
        m_desc = "Target friendly entity gets +2 health and +2 current action points.";
        m_playDesc = "The target is infused with fortitude!";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddMaxHealth(2);
        targetEntity.GainAP(2);
    }
}
