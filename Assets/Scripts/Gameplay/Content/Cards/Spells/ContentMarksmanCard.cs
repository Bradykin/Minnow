using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarksmanCard : GameCardSpellBase
{
    private int m_powerBuff = 3;

    public ContentMarksmanCard()
    {
        m_name = "Marksman";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Rare;

        m_cost = 2;

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        return "Target allied unit with at least <b>Range</b> 2 <b>permanently</b> gains +" + m_powerBuff + "/+0.";
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay() && targetUnit.GetRange() >= 2;
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        targetUnit.AddStats(m_powerBuff, 0, true, true);
    }
}
