using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentMarksmanCard : GameCardSpellBase
{
    public ContentMarksmanCard()
    {
        m_name = "Marksman";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Rare;
        m_spellEffect = 3;

        m_cost = 2;

        SetupBasicData();

        m_onPlaySFX = AudioHelper.BowSpell;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return $"Target allied unit with at least <b>Range</b> 2 <b>permanently</b> gains +{UIHelper.GetMagicPowerColoredValue(m_spellEffect + mpString)}/+0.";
    }

    public override bool IsValidToPlay(GameUnit targetUnit)
    {
        return base.IsValidToPlay(targetUnit) && targetUnit.GetRange() >= 2;
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
