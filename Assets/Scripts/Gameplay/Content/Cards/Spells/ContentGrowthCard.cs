using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrowthCard : GameCardSpellBase
{
    public ContentGrowthCard()
    {
        m_name = "Growth";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;
        m_spellEffect = 4;

        SetupBasicData();

        m_tagHolder.AddPushTag(GameTagHolder.TagType.Forest);
        m_tagHolder.AddReceiverOnlyTag(GameTagHolder.TagType.BuffSpell);

        m_onPlaySFX = AudioHelper.TreeGrow;
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetGameTile().GetTerrain().IsForest();
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit in a forest gains +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}/+{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}.";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(GetSpellValue(), GetSpellValue(), false, true);
    }
}
