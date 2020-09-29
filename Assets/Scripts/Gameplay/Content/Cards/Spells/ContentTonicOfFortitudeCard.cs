using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfFortitudeCard : GameCardSpellBase
{
    private int m_healthGain = 10;
    private int m_apGain = 2;

    public ContentTonicOfFortitudeCard()
    {
        m_name = "Tonic of Fortitude";
        m_desc = "Target allied unit gains +0/+" + m_healthGain + " and +" + m_apGain + " AP.";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Common;
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

        targetEntity.AddMaxHealth(m_healthGain);
        targetEntity.GainAP(m_apGain);
    }
}
