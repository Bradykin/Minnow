using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfFortitudeCard : GameCardSpellBase
{
    private int m_healthGain = 10;

    public ContentTonicOfFortitudeCard()
    {
        m_name = "Tonic of Fortitude";
        m_desc = "Target allied unit <b>permanently</b> gains +0/+" + m_healthGain + ".";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(0, m_healthGain, true, true);
    }
}
