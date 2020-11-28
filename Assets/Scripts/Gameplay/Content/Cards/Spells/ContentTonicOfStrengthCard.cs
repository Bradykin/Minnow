using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfStrengthCard : GameCardSpellBase
{
    private int m_powerToGain = 3;
    private int m_staminaGain = 2;

    public ContentTonicOfStrengthCard()
    {
        m_name = "Tonic of Strength";
        m_desc = "Target allied unit <b>permanently</b> gains +" + m_powerToGain + "/+0 and +" + m_staminaGain + " Stamina.";
        m_targetType = Target.Ally;
        m_cost = 3;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(m_powerToGain, 0, true, true);
        targetUnit.GainStamina(m_staminaGain);
    }
}
