using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBladesCard : GameCardSpellBase
{
    public ContentBladesCard()
    {
        m_name = "Blades";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;
        m_spellEffect = 8;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.StaminaRegen);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.DaggerSwingSpell;
    }


    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit gains +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}/+0.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(GetSpellValue(), 0, false, true);
    }
}
