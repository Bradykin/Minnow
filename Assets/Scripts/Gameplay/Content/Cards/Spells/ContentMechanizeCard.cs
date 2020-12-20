using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMechanizeCard : GameCardSpellBase
{
    public ContentMechanizeCard()
    {
        m_name = "Mechanize";
        m_desc = "Target allied unit loses all current Stamina and <b>permanently</b> gains that much power.";
        m_targetType = Target.Ally;
        m_cost = 0;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.Creation);
        m_tags.AddTag(GameTag.TagType.MaxStamina);
        m_tags.AddTag(GameTag.TagType.StaminaRegen);
        m_tags.AddTag(GameTag.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.MetalBuff;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        int curStamina = targetUnit.GetCurStamina();
        targetUnit.SpendStamina(curStamina);
        targetUnit.AddStats(curStamina, 0, true, false);
    }
}