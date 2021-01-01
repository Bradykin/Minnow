using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentExperienceCard : GameCardSpellBase
{
    public ContentExperienceCard()
    {
        m_name = "Experience";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Uncommon;
        m_xSpell = true;
        m_shouldExile = true;

        m_spellEffect = 1;

        m_cost = 0;

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        SetupBasicData();

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit <b>permanently</b> gains +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}X/+{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}X.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        int yVal = GameHelper.GetPlayer().GetXValue() * GetSpellValue();

        base.PlayCard(targetUnit);

        targetUnit.AddStats(yVal, yVal, true, true);
    }
}
