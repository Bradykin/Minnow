using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentTonicOfStrengthCard : GameCardSpellBase
{
    public ContentTonicOfStrengthCard()
    {
        m_name = "Tonic of Strength";
        m_targetType = Target.Ally;
        m_cost = 2;
        m_rarity = GameRarity.Common;
        m_shouldExile = true;
        m_spellEffect = 5;

        SetupBasicData();

        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.SmallBuff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit <b>permanently</b> gains +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}/+0.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(GetSpellValue(), 0, true, true);
    }
}
