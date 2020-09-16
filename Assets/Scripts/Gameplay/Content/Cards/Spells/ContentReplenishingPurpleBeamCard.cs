using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentReplenishingPurpleBeamCard : GameCardSpellBase
{
    public ContentReplenishingPurpleBeamCard()
    {
        m_spellEffect = 2;

        m_name = "Replenishing Purple Beam";
        m_playDesc = "Very purple.  There's so, so much purple beam.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Uncommon;

        SetupBasicData();
    }

    public override string GetDesc()
    {
        string spString = "";
        if (HasSpellPower())
        {
            spString = GetSpellPowerString();
        }

        return "Add " + m_spellEffect + spString + " to the purple beam count.\n" + GetModifiedBySpellPowerString();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        Globals.m_purpleBeamCount += GetSpellValue();
    }
}
