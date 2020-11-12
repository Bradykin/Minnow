using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentGrowTalonsCard : GameCardSpellBase
{
    public ContentGrowTalonsCard()
    {
        m_name = "Grow Talons";
        m_targetType = Target.Ally;
        m_rarity = GameRarity.Starter;
        m_shouldExile = true;

        m_cost = 1;
        m_spellEffect = 2;

        SetupBasicData();

        m_audioCategory = AudioHelper.SpellAudioCategory.Buff;
    }

    public override string GetDesc()
    {
        string mpString = "";
        if (HasMagicPower())
        {
            mpString = GetMagicPowerString();
        }

        return "Give an allied unit +" + m_spellEffect + mpString + "/+0.\n" + GetModifiedByMagicPowerString() + "\n\n<i>(Buffs are permanent)</i>";
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        int powerToAdd = GetSpellValue();
        targetUnit.AddStats(powerToAdd, 0);
    }
}
