using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfStrengthCard : GameCardSpellBase
{
    private int m_powerToGain = 5;
    private int m_apGain = 2;

    public ContentTonicOfStrengthCard()
    {
        m_name = "Tonic of Strength";
        m_desc = "Target friendly entity gets +" + m_powerToGain + " power and +" + m_apGain + " current action points.";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.APRegen);
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.AddPower(m_powerToGain);
        targetEntity.GainAP(m_apGain);
    }
}
