using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBlastingPurpleBeamCard : GameCardSpellBase
{
    public ContentBlastingPurpleBeamCard()
    {
        m_spellEffect = 6;

        m_name = "Blasting Purple Beam";
        m_playDesc = "Very purple.  Much beam.";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        return GetDamageDescString() + "If it kills the entity, increase the purple beam count by 1.";
    }

    public override void PlayCard(GameEntity targetEntity)
    {
        if (!IsValidToPlay(targetEntity))
        {
            return;
        }

        base.PlayCard(targetEntity);

        targetEntity.GetHit(GetSpellValue());

        if (targetEntity.m_isDead)
        {
            Globals.m_purpleBeamCount++;
        }
    }
}
