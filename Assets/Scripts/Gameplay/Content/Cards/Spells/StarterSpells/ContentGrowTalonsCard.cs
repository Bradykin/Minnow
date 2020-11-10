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

        InitializeWithLevel(GetCardLevel());

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

        if (GetCardLevel() >= 2)
        {
            return "Give an allied unit +" + m_spellEffect + mpString + "/+" + m_spellEffect + mpString + ".\n" + GetModifiedByMagicPowerString() + "\n\n<i>(Buffs are permanent)</i>";
        }
        else
        {
            return "Give an allied unit +" + m_spellEffect + mpString + "/+0.\n" + GetModifiedByMagicPowerString() + "\n\n<i>(Buffs are permanent)</i>";
        }
    }

    public override void PlayCard(GameUnit targetUnit)
    {
        if (!IsValidToPlay(targetUnit))
        {
            return;
        }

        base.PlayCard(targetUnit);

        int powerToAdd = GetSpellValue();
        int healthToAdd = 0;
        if (GetCardLevel() >= 2)
        {
            healthToAdd = GetSpellValue();
        }
        targetUnit.AddStats(powerToAdd, healthToAdd);
    }

    public override void InitializeWithLevel(int level)
    {
        m_cost = 1;
        m_spellEffect = 2;

        if (level >= 1)
        {
            m_spellEffect = 3;
        }

        if (level >= 2)
        {
            //Also buff health
        }
    }
}
