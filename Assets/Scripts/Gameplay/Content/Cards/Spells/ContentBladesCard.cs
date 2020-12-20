using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBladesCard : GameCardSpellBase
{
    private int m_powerToGain = 8;

    public ContentBladesCard()
    {
        m_name = "Blades";
        m_desc = "Target allied unit gains +" + m_powerToGain + "/+0.";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(m_powerToGain, 0, false, true);
    }
}
