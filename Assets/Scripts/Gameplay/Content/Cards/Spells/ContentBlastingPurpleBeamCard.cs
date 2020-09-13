using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBlastingPurpleBeamCard : GameCardSpellBase
{
    public ContentBlastingPurpleBeamCard()
    {
        m_spellEffect = 4;

        m_name = "Blasting Purple Beam";
        m_desc = "Blast an enemy for " + GetSpellValue() + " damage.  If it kills the entity, increase the purple beam count by 1.";
        m_playDesc = "Very purple.  Much beam.";
        m_targetType = Target.Entity;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
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
