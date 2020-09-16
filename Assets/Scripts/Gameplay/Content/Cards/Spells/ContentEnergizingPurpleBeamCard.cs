using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentEnergizingPurpleBeamCard : GameCardSpellBase
{
    public ContentEnergizingPurpleBeamCard()
    {
        m_spellEffect = 1;

        m_name = "Energizing Purple Beam";
        m_desc = "Restore energy equal to the purple beam count.  -1 to the purple beam count.";
        m_playDesc = "Very purple.  So much beam.";
        m_targetType = Target.None;
        m_cost = 0;
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

        GameHelper.GetPlayer().AddEnergy(Globals.m_purpleBeamCount);

        Globals.m_purpleBeamCount--;
    }
}

