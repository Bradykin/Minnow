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
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        if (GetCardLevel() >= 2)
        {
            return "Give an allied unit +" + m_spellEffect + spString + "/+" + m_spellEffect + spString + ".\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
        }
        else
        {
            return "Give an allied unit +" + m_spellEffect + spString + "/+0.\n" + GetModifiedBySpellPowerString() + "\n\n<i>(Buffs are permanent)</i>";
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
            m_spellEffect = 4;
        }

        if (level >= 2)
        {
            //Also buff health
        }
    }
}
