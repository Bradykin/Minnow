using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentOrbOfEnergyRelic : GameRelic
{
    public ContentOrbOfEnergyRelic()
    {
        SetRelicLevel(GetRelicLevel());

        m_name = "Orb of Energy";
        m_desc = "Increase max energy by " + (m_relicLevel + 1) + ".";
        m_rarity = GameRarity.Starter;

        LateInit();
    }
}
