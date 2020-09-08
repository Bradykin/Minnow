using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentReplenishingPurpleBeamCard : GameCardSpellBase
{
    public ContentReplenishingPurpleBeamCard()
    {
        m_name = "Replenishing Purple Beam";
        m_desc = "Add 3 to the purple beam count.";
        m_playDesc = "Very purple.  There's so, so much purple beam.";
        m_targetType = Target.None;
        m_cost = 1;
        m_rarity = GameRarity.Common;

        SetupBasicData();
    }

    public override void PlayCard()
    {
        if (!IsValidToPlay())
        {
            return;
        }

        base.PlayCard();

        Globals.m_purpleBeamCount += 3;
    }
}
