using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfStrengthCard : GameCardSpellBase
{
    private int m_powerToGain = 5;

    public ContentTonicOfStrengthCard()
    {
        m_name = "Tonic of Strength";
        m_desc = "Target allied unit <b>permanently</b> gains +" + m_powerToGain + "/+0.";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(m_powerToGain, 0, true, true);
    }
}
