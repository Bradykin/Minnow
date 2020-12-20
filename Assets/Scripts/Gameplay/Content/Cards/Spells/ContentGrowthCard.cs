using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrowthCard : GameCardSpellBase
{
    private int m_statBuff = 4;

    public ContentGrowthCard()
    {
        m_name = "Growth";
        m_targetType = Target.Ally;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();

        m_tags.AddTag(GameTag.TagType.BuffSpell);
        m_tags.AddTag(GameTag.TagType.Forest);

        m_onPlaySFX = AudioHelper.TreeGrow;
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetGameTile().GetTerrain().IsForest();
    }

    public override string GetDesc()
    {
        return "Target allied unit in a forest gains +" + m_statBuff + "/+" + m_statBuff + ".";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(m_statBuff, m_statBuff, false, true);
    }
}
