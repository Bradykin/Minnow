using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentHeroismCard : GameCardSpellBase
{
    private int m_statBuff = 15;

    public ContentHeroismCard()
    {
        m_name = "Heroism";
        m_targetType = Target.Ally;
        m_cost = 4;
        m_rarity = GameRarity.Uncommon;
        m_shouldExile = true;
        m_spellEffect = 1;

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.MagicEffect;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gains +{m_statBuff}/+{m_statBuff} and +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)} stamina regen.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(m_statBuff, m_statBuff, false, true);
        targetUnit.AddStaminaRegen(GetSpellValue(), false);
    }
}