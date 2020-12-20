using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeroismCard : GameCardSpellBase
{
    private int m_statBuff = 15;
    private int m_stamRegenBuff = 1;

    public ContentHeroismCard()
    {
        m_name = "Heroism";
        m_targetType = Target.Ally;
        m_cost = 4;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.MagicEffect;
    }

    public override string GetDesc()
    {
        return "Target allied unit gains +" + m_statBuff + "/+" + m_statBuff + " and +" + m_stamRegenBuff + " stamina regen.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(m_statBuff, m_statBuff, false, true);
        targetUnit.AddStaminaRegen(m_stamRegenBuff, false);
    }
}