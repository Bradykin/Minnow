using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfStrengthCard : GameCardSpellBase
{
    public ContentTonicOfStrengthCard()
    {
        m_name = "Tonic of Strength";
        m_desc = "Target friendly entity gets +2 power and +2 current action points.";
        m_playDesc = "The target is infused with strength!";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;
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

        targetEntity.AddPower(2);
        targetEntity.GainAP(2);
    }
}
